# UI Dark Theme Refactor — Option D (Hybrid)

## Goal

Refactor all 4 WinForms forms (`MainForm`, `PhoneSenderForm`, `MessageForm`, `CredentialsForm`) with a modern dark theme, responsive layout, and modernized controls — while keeping all `.Designer.cs` files 100% VS Designer compatible (no loops, no lambdas).

## Decisions

| Decision | Choice | Why |
|----------|--------|-----|
| Theme | Dark (`#1E1E2E` base) | User preference |
| Layout | Responsive (Dock/Anchor) | User preference |
| Controls | Modernized (ToolStrip, CheckedListBox, StatusStrip) | User preference |
| Libs | None (pure WinForms) | Zero dependency risk |
| Designer files | Layout + containers only | VS Designer compat |
| Styling | `ThemeManager.cs` in code-behind | Centralized, reusable |

## Color Palette

| Role | Hex | Usage |
|------|-----|-------|
| **Base BG** | `#1E1E2E` | Form backgrounds |
| **Surface** | `#282A3A` | Panels, GroupBoxes, cards |
| **Surface Alt** | `#2E3044` | Alternate rows, hover states |
| **Border** | `#3A3C50` | Panel borders, grid lines |
| **Text Primary** | `#E4E4E8` | Labels, body text |
| **Text Secondary** | `#9CA3AF` | Hints, secondary info |
| **Accent** | `#00BFA6` (Teal) | Buttons, active states, highlights |
| **Accent Hover** | `#00E5C3` | Button hover |
| **Danger** | `#EF4444` | Cancel, delete, errors |
| **Danger Hover** | `#F87171` | Danger hover |
| **Warning** | `#F59E0B` | Scheduler status, warnings |
| **Success** | `#22C55E` | Success indicators |
| **Selection** | `#00BFA633` (Teal 20%) | DataGridView selection |
| **Header BG** | `#1A1A2E` | DataGridView column headers |

## Tasks

### ✅ Task 1: Create `ThemeManager.cs`
Create `Theme/ThemeManager.cs` — a static class with:
- Color constants for the entire dark palette above
- `ApplyTheme(Form form)` — recursively walks controls and applies dark colors
- `StyleButton(Button btn, bool isPrimary, bool isDanger)` — sets `FlatStyle.Flat`, colors, padding, cursor
- `StyleDataGridView(DataGridView dgv)` — headers, alternating rows, selection, grid colors, border
- `StyleTextBox(TextBox tb)` — dark bg, light text, border color
- `StyleLabel(Label lbl)` — light text color
- `StyleGroupBox(GroupBox gb)` — surface bg, lighter text title
- `StyleComboBox(ComboBox cb)` — dark bg, light text
- `StyleCheckBox(CheckBox cb)` / `StyleCheckedListBox(CheckedListBox clb)` — forecolor
- `StyleProgressBar(ProgressBar pb)` — custom colors via style override
- `StyleToolStrip(ToolStrip ts)` — dark renderer
- `StyleStatusStrip(StatusStrip ss)` — dark bg, light text
- `StyleListBox(ListBox lb)` — dark bg, light text
- Font: `Segoe UI, 9.75pt` for body, `Segoe UI Semibold, 11pt` for titles

→ **Verify:** File compiles. No loops/lambdas — only iterating in the `ApplyTheme` method (which is code-behind, not designer).

---

### ✅ Task 2: Create `Theme/DarkToolStripRenderer.cs`
Custom `ToolStripProfessionalRenderer` subclass for dark ToolStrip/MenuStrip:
- Override `OnRenderToolStripBackground` — dark bg
- Override `OnRenderMenuItemBackground` — hover color
- Override `OnRenderItemText` — light text
- Override `OnRenderSeparator` — subtle border color

→ **Verify:** File compiles.

---

### ✅ Task 3: Refactor `MainForm.Designer.cs`
Restructure layout using containers. **No loops, no lambdas.**

**New layout structure:**
```
MainForm (Dock: Fill)
├── toolStripMain (Dock: Top)
│     Items: Fetch, Send Message, Open Sender, Cancel, Export, Credentials
├── panelTopBar (Dock: Top, ~80px)
│     ├── comboBoxExpirationStatus
│     ├── textBoxSearch (with placeholder)
│     ├── labelUserCount
│     └── buttonRemoveRows
├── panelScheduler (Dock: Top, ~60px) — GroupBox "Scheduler"
│     ├── timePicker
│     ├── dateTimePicker
│     ├── checkedListBoxDays (replaces 7 checkboxes)
│     ├── buttonStartScheduler
│     ├── buttonStopScheduler
│     └── labelSchedulerStatus
├── dataGridViewResults (Dock: Fill)
└── statusStripBottom (Dock: Bottom)
      ├── toolStripStatusLabelProgress (replaces labelProgress)
      ├── toolStripProgressBar (replaces progressBarSending)
      └── toolStripStatusLabelSuccessFail (replaces labelSuccessFailure)
```

**Controls removed:** 7 individual `CheckBox` controls, `progressBarSending`, `labelProgress`, `labelSuccessFailure`
**Controls added:** `ToolStrip`, `StatusStrip`, `CheckedListBox`, `GroupBox`, `Panel`

**Designer rules:**
- All `new Control()`, `.Location`, `.Size`, `.Dock`, `.Anchor`, `.Name`, `.Text`, `.TabIndex` in designer
- All `Controls.Add()` in designer
- Event handlers wired with `+=` (no `new EventHandler(...)` wrapper needed in .NET 8)
- `CheckedListBox.Items.AddRange(new object[] { "Sat", "Sun", "Mon", ... })` — this is allowed (it's not a loop/lambda)

→ **Verify:** Open in VS Designer without errors. All controls visible and draggable.

---

### ✅ Task 4: Update `MainForm.cs` code-behind
- Call `ThemeManager.ApplyTheme(this)` in constructor after `InitializeComponent()`
- Replace `checkBoxMonday`, etc. logic with `CheckedListBox` index-based logic
- Update `checkBoxDay_CheckedChanged` → `checkedListBoxDays_ItemCheck` event
- Update `LoadSelectedDaysFromSettings` / `SaveSelectedDaysToSettings` for `CheckedListBox`
- Update `UpdateProgressAndLabels` to write to `StatusStrip` labels instead of standalone labels
- Update `ResetProgressAndLabels` similarly
- Remove all hardcoded `Color.White` / `Color.Yellow` / `Color.DarkGray` — use `ThemeManager` palette colors
- Search highlighting: use `ThemeManager.SurfaceAlt` for match, `ThemeManager.BaseBg` for no match

→ **Verify:** Build succeeds. Fetch data, search, send, scheduler all work as before.

---

### ✅ Task 5: Refactor `PhoneSenderForm.Designer.cs`
Restructure layout with containers:

```
PhoneSenderForm (Size: 340x700, responsive)
├── panelPhoneList (Dock: Top, ~280px)
│     ├── labelPhoneNumbers
│     ├── listBoxPhoneNumbers (Dock: Fill inside panel)
│     ├── panelPhoneActions (Dock: Bottom, ~35px)
│     │     ├── labelPhoneNumberCount
│     │     └── buttonClear
├── panelMessage (Dock: Top, ~120px) — GroupBox "Message"
│     ├── labelMessage
│     └── textBoxMessage (Dock: Fill inside group)
├── panelActions (Dock: Top, ~35px)
│     ├── buttonSend
│     ├── buttonInject
│     └── buttonRemove
├── panelCustom (Dock: Top, ~140px) — GroupBox "Custom Numbers"
│     ├── checkBoxCustomNumbers
│     ├── labelCustomNumberCount
│     └── textBoxCustomNumbers
├── panelInject (Dock: Top, ~120px) — GroupBox "Inject Numbers"
│     ├── labelInjectNumbers
│     └── textBoxInjectNumbers
├── labelStatus (Dock: Bottom)
└── progressBarSending (Dock: Bottom)
```

→ **Verify:** Opens in VS Designer. All controls present.

---

### ✅ Task 6: Update `PhoneSenderForm.cs` code-behind
- Call `ThemeManager.ApplyTheme(this)` in constructor
- No logic changes needed (functionality stays the same)

→ **Verify:** Build succeeds. Send, inject, remove, clear all work.

---

### ✅ Task 7: Refactor `MessageForm.Designer.cs`
Simple restructure:

```
MessageForm (Size: 420x480)
├── labelInstruction (Dock: Top, padding)
├── textBoxMessage (Dock: Fill)
└── panelButtons (Dock: Bottom, ~50px)
      └── buttonSend (Anchor: Bottom,Right)
```

→ **Verify:** Opens in VS Designer.

---

### ✅ Task 8: Update `MessageForm.cs` code-behind
- Call `ThemeManager.ApplyTheme(this)` in constructor

→ **Verify:** Build succeeds. Message form opens, saves template.

---

### ✅ Task 9: Refactor `CredentialsForm.Designer.cs`
Already uses `Panel` + anchoring. Improve:
- Wrap `DataGridView` and button panel in proper Dock layout
- Ensure responsive resize with `Dock.Fill` on grid

→ **Verify:** Opens in VS Designer.

---

### ✅ Task 10: Update `CredentialsForm.cs` code-behind
- Call `ThemeManager.ApplyTheme(this)` in constructor
- Style the `DataGridView` columns with dark theme

→ **Verify:** Build succeeds. Add, delete, save credentials all work.

---

### ✅ Task 11: Final Build + Smoke Test
- `dotnet build` — zero errors, zero warnings
- Run the app:
  - MainForm: dark theme applied, all buttons styled, DataGridView dark, ToolStrip works, StatusStrip shows progress
  - Fetch Data: data loads into dark-styled grid
  - Search: highlights with dark-compatible colors
  - Scheduler: CheckedListBox days work, start/stop scheduler
  - Open Sender Form: dark theme, responsive
  - Credentials: dark theme, CRUD works  
  - Message Form: dark theme, send works
  - Export to Excel: still works
  - All forms resize properly (no overlapping controls)

→ **Verify:** App runs end-to-end without errors. All forms look modern and dark.

## File Map

| File | Action | Contains |
|------|--------|----------|
| `Theme/ThemeManager.cs` | **NEW** ✅ | All dark theme colors + styling methods |
| `Theme/DarkToolStripRenderer.cs` | **NEW** ✅ | Custom ToolStrip renderer |
| `Forms/MainForm.Designer.cs` | **REWRITE** ✅ | Restructured layout with ToolStrip, Panels, StatusStrip, CheckedListBox |
| `Forms/MainForm.cs` | **EDIT** ✅ | ThemeManager call + CheckedListBox logic + StatusStrip updates |
| `Forms/PhoneSenderForm.Designer.cs` | **REWRITE** ✅ | Restructured layout with GroupBoxes, Panels |
| `Forms/PhoneSenderForm.cs` | **EDIT** ✅ | ThemeManager call |
| `Forms/MessageForm.Designer.cs` | **REWRITE** ✅ | Restructured layout with Dock |
| `Forms/MessageForm.cs` | **EDIT** ✅ | ThemeManager call |
| `Forms/CredentialsForm.Designer.cs` | **EDIT** ✅ | Improve dock layout |
| `Forms/CredentialsForm.cs` | **EDIT** ✅ | ThemeManager call |

## Notes

- All `.Designer.cs` files: NO `for`, `foreach`, `while`, NO lambdas (`=>`), NO LINQ. Only `new`, property assignment, `Controls.Add()`, and `Items.AddRange()`.
- All styling logic (recursive control walking, conditional coloring) goes in `ThemeManager.cs` and `.cs` code-behind.
- `DoubleBuffered = true` should be set on forms in code-behind for flicker-free rendering.
- ToolStrip buttons replace standalone buttons → fewer controls, cleaner surface.
- StatusStrip replaces floating labels + progress bar → standard Windows UX pattern.

## Done When

- [x] All 4 forms have dark theme applied
- [x] All forms resize properly without overlapping
- [x] VS Designer opens all `.Designer.cs` without errors
- [x] Zero `for`/`foreach`/`while`/lambda in any `.Designer.cs`
- [ ] All existing functionality works (fetch, send, schedule, export, credentials) — needs manual smoke test
- [x] `dotnet build` passes with zero errors
- [x] Forms moved to `Forms/` folder
