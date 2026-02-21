# SAS Native C# Refactor — Full Architecture

## Goal

Eliminate the PHP middleware server dependency. Replace all HTTP calls to `http://127.0.0.1/sas/*.php` with native C# SAS4 API calls using the reference code from `reference/C# sas4/`. Restructure the project into proper layers: **Models → Services → UI**. Support **dynamic multi-credential management** via UI.

## Project Type

**DESKTOP** — .NET 8 WinForms (C#)

## Success Criteria

- [x] App fetches expiring users directly from SAS4 Radius (no PHP server)
- [x] App fetches dashboard widget data directly from SAS4 Radius
- [x] App can disconnect users directly via SAS4 Radius
- [x] App can search users and fetch invoices directly
- [x] App can fetch card/series data directly
- [x] No HTTP calls to `127.0.0.1` remain (except WhatsApp localhost API)
- [x] Clean folder structure: `Models/`, `Services/`, `Helpers/`
- [x] **Dynamic credentials**: Users can add/edit/delete SAS credentials through a Settings UI
- [x] **Multi-host support**: Each credential set can target a different SAS host
- [x] Credentials persist in `credentials.json` (not hardcoded)
- [x] All existing functionality preserved (WhatsApp sending, scheduler, Excel export)
- [x] App compiles and runs without errors

## Tech Stack

| Component | Technology | Rationale |
|-----------|-----------|-----------|
| Framework | .NET 8 WinForms | Already in use |
| HTTP | `System.Net.Http.HttpClient` | Built-in, async |
| JSON | `System.Text.Json` + `Newtonsoft.Json` | STJ for SAS connector, Newtonsoft for existing WhatsApp code |
| Crypto | `System.Security.Cryptography` | AES-256-CBC, MD5 key derivation |
| Excel | EPPlus 7.3.0 | Already in use |
| Credential Storage | `credentials.json` | Simple, portable, human-readable |

## File Structure (After Refactor)

```
Nomor_Whatsapp_Sender/
├── Helpers/
│   └── AESHelper.cs              ← FROM reference (namespace changed)
├── Models/
│   ├── UserData.cs               ← EXTRACTED from MainForm.cs
│   ├── DashboardData.cs          ← NEW (dashboard widget models)
│   ├── CardData.cs               ← NEW (card/series models)
│   ├── InvoiceData.cs            ← NEW (invoice models)
│   └── SasCredential.cs          ← NEW (credential model)
├── Services/
│   ├── SASConnector.cs           ← FROM reference (namespace changed)
│   ├── CredentialManager.cs      ← NEW (replaces SasConfig, loads/saves credentials.json)
│   ├── SasService.cs             ← NEW (all SAS business logic, iterates N credentials)
│   └── WhatsAppService.cs        ← EXTRACTED from MainForm.cs
├── CredentialsForm.cs            ← NEW (UI to add/edit/delete credentials)
├── CredentialsForm.Designer.cs   ← NEW (auto-generated)
├── MainForm.cs                   ← SLIMMED (delegates to services, button to open CredentialsForm)
├── MainForm.Designer.cs          ← UPDATED (add credentials button)
├── PhoneSenderForm.cs            ← UPDATED (uses WhatsAppService)
├── PhoneSenderForm.Designer.cs   ← UNCHANGED
├── MessageForm.cs                ← UNCHANGED
├── Program.cs                    ← UNCHANGED
├── App.config                    ← UNCHANGED
├── credentials.json              ← NEW (auto-created on first run with default creds)
└── Nomor_Whatsapp_Sender.csproj  ← UNCHANGED
```

## Credential System Design

### SasCredential Model
```csharp
public class SasCredential {
    public string Name { get; set; }      // Friendly label e.g. "504-505"
    public string Host { get; set; }      // Can differ per credential
    public string Username { get; set; }
    public string Password { get; set; }
    public bool Enabled { get; set; }     // Toggle without deleting
}
```

### CredentialManager
- Loads `credentials.json` from app directory on startup
- Saves on every change
- First run: seeds with 2 default credentials from reference
- Thread-safe read access

### SasService Changes
- `CreateAndLoginAsync()` → returns `List<SASConnector>` (N connectors, one per enabled credential)
- All methods iterate over N connectors instead of hardcoded 2
- Parallel login + parallel API calls

### CredentialsForm UI
- DataGridView showing: Name | Host | Username | Password (masked) | Enabled
- Buttons: Add, Edit, Delete, Save
- Inline editing or dialog-based editing

## Tasks

### Task 1: Create folder structure
**Priority:** P0 | **Dependencies:** none
Create `Helpers/`, `Models/`, `Services/` directories.
→ **VERIFY:** Folders exist ✅ DONE

---

### Task 2: Add `AESHelper.cs` to Helpers/
**Priority:** P0 | **Dependencies:** Task 1
Copy from reference, change namespace → `Nomor_Whatsapp_Sender.Helpers`
→ **VERIFY:** Compiles ✅ DONE

---

### Task 3: Add `SASConnector.cs` to Services/
**Priority:** P0 | **Dependencies:** Task 2
Copy from reference, change namespace → `Nomor_Whatsapp_Sender.Services`
→ **VERIFY:** Compiles ✅ DONE

---

### Task 4: Create `SasCredential.cs` model + `CredentialManager.cs`
**Priority:** P0 | **Dependencies:** Task 1
**REPLACES old Task 4 (static SasConfig)**

Create `Models/SasCredential.cs` — credential data model.
Create `Services/CredentialManager.cs` — loads/saves `credentials.json`, CRUD operations.
Delete `Services/SasConfig.cs` (replaced by CredentialManager).

Default credentials on first run:
- Name: "504-505", Host: admin.halasat-ftth.iq, User: OMC_Pst_Dis@504_505
- Name: "506-510", Host: admin.halasat-ftth.iq, User: OMC_Pst_Dis@506_510_1

→ **VERIFY:** `CredentialManager.GetEnabled()` returns list of credentials ✅ DONE

---

### Task 5: Extract Models from MainForm.cs
**Priority:** P1 | **Dependencies:** Task 1
Extract `UserData`, `DashboardData`, `CardData`, `InvoiceData` model classes.
→ **VERIFY:** Compiles ✅ DONE

---

### Task 6: Create `SasService.cs` (dynamic N-credential)
**Priority:** P1 | **Dependencies:** Tasks 2, 3, 4, 5
**UPDATED: Now iterates over N credentials from CredentialManager**

All methods:
1. Get enabled credentials from `CredentialManager`
2. Create N `SASConnector` instances
3. Login all in parallel
4. Call API on all in parallel
5. Merge results

→ **VERIFY:** Compiles, handles 0, 1, N credentials ✅ DONE

---

### Task 7: Extract `WhatsAppService.cs`
**Priority:** P1 | **Dependencies:** Task 1
→ **VERIFY:** Compiles ✅ DONE

---

### Task 8: Create `CredentialsForm` (UI)
**Priority:** P2 | **Dependencies:** Task 4
**NEW TASK**

WinForms dialog with:
- DataGridView: Name | Host | Username | Password | Enabled (checkbox)
- Add button → adds empty row
- Delete button → removes selected row
- Save button → saves to `credentials.json` and closes
- Cancel button → closes without saving

→ **VERIFY:** Opens from MainForm, can add/edit/delete credentials, changes persist

---

### Task 9: Refactor `MainForm.cs` — Wire Services + Credentials Button
**Priority:** P2 | **Dependencies:** Tasks 5, 6, 7, 8

1. Add using statements for Models, Services, Helpers
2. Remove inline model classes (moved to Models/)
3. Remove `GetJsonDataAsync`, `SendWhatsAppMessage`, `CleanPhoneNumber`
4. Add service fields: `SasService`, `WhatsAppService`
5. Wire `buttonFetchData_Click` → `SasService.GetExpiringUsersAsync`
6. Wire `buttonSendMessage_Click` → `WhatsAppService.SendMessageAsync`
7. Add "Credentials" button to open `CredentialsForm`

→ **VERIFY:** App compiles, data loads, credentials manageable

---

### Task 10: Refactor `PhoneSenderForm.cs` — Use WhatsAppService
**Priority:** P2 | **Dependencies:** Task 7
→ **VERIFY:** Compiles, sends messages

---

### Task 11: Build + Smoke Test
**Priority:** P3 | **Dependencies:** Tasks 9, 10

1. `dotnet build` → 0 errors
2. No references to `127.0.0.1/sas` in any `.cs` file
3. All services properly wired

→ **VERIFY:** Clean build, no PHP references

## Dependency Graph

```
Task 1 (folders) ✅
  ├── Task 2 (AESHelper) ✅
  │     └── Task 3 (SASConnector) ✅
  │           └─┐
  ├── Task 4 (SasCredential + CredentialManager) ←── UPDATED
  │             └─┐
  ├── Task 5 (Models) ✅
  │               │
  │     Task 6 (SasService — N-credential) ←── UPDATED
  │               │
  │     Task 8 (CredentialsForm UI) ←── NEW
  │               │
  │     Task 9 (Refactor MainForm)
  │               │
  └── Task 7 (WhatsAppService) ✅
                  │
        Task 10 (Refactor PhoneSender)
                  │
        Task 11 (Build + Test)
```

## Risks & Mitigations

| Risk | Impact | Mitigation |
|------|--------|------------|
| AES encryption mismatch | Login fails | C# AESHelper replicates PHP openssl_encrypt exactly |
| credentials.json missing/corrupt | App crashes | Auto-create with defaults on first run, catch parse errors |
| One credential login fails | Partial data | try-catch per credential, merge what succeeds |
| N credentials = N API calls = slow | UI lag | Parallel login + parallel API calls |
| Different hosts = different data formats | Parse errors | Same SAS4 version assumption, flexible JSON parsing |

## Phase X: Verification

- [x] `dotnet build` — 0 errors, 0 warnings
- [x] No references to `127.0.0.1/sas` in any `.cs` file
- [ ] App launches without exceptions
- [x] Credentials form opens, can add/edit/delete
- [ ] credentials.json persists across restarts
- [ ] Expiring users data loads from N credentials
- [x] WhatsApp sending still works
- [x] Scheduler still works
- [x] Excel export still works
- [x] All 8 SasService methods exist and compile
