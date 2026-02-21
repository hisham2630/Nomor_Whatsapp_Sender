# Nomor WhatsApp Sender

A Windows desktop application for sending bulk WhatsApp messages to subscribers with expiring or expired internet subscriptions. Built with .NET 8 WinForms.

## Features

- **Dual Message Templates** — Separate templates for active (expiring soon) and expired subscriptions, auto-selected per subscriber based on remaining days
- **Placeholder System** — Dynamic message personalization with `%CustomerName%`, `%Expiration%`, `%BundleName%`, `%يوم%`, `%ساعة%`, `%دقيقة%`
- **SAS Integration** — Fetches subscriber data from SAS/Radius API with multi-credential support
- **Scheduler** — Automated sending on selected days/times
- **Phone Sender Form** — Manual bulk sending with inject/remove/custom number support
- **Dark Theme** — Full dark mode UI
- **Excel Export** — Export subscriber data to `.xlsx`
- **Location Sharing** — Optional GPS coordinates with each message

## Screenshot

> *Run the app to see the dark-themed UI with data grid, scheduler, and template editor.*

## Requirements

- Windows 10/11
- .NET 8.0 Runtime (or build as self-contained)

## Setup

1. **Clone the repo**
   ```bash
   git clone https://github.com/hisham2630/Nomor_Whatsapp_Sender.git
   ```

2. **Build**
   ```bash
   cd Nomor_Whatsapp_Sender
   dotnet build
   ```

3. **Run**
   ```bash
   dotnet run
   ```

4. **Add Credentials** — On first run, click 🔑 **Credentials** in the toolbar to add your SAS API credentials.

5. **Configure WhatsApp API** — Set your WhatsApp API URL in the API URL field (default: `http://localhost:3111/send?number={phone}&message={message}`)

## Message Templates

The app uses two templates that are auto-selected based on subscription status:

| Template | Used When | Description |
|----------|-----------|-------------|
| **Active** | `Remaining_days > 0` | Subscription expiring soon |
| **Expired** | `Remaining_days ≤ 0` | Subscription already expired |

### Available Placeholders

| Placeholder | Description |
|-------------|-------------|
| `%CustomerName%` | Subscriber's first name |
| `%Expiration%` | Expiration date (formatted) |
| `%BundleName%` | Subscription profile name |
| `%يوم%` | Days remaining/elapsed |
| `%ساعة%` | Hours remaining/elapsed |
| `%دقيقة%` | Minutes remaining/elapsed |

## WhatsApp API

The app expects a WhatsApp API endpoint that accepts GET requests:

```
GET /send?number={phone}&message={message}&location={lat,lng}
```

**Success response:**
```json
{
  "success": true,
  "message": "Message sent successfully to number",
  "jid": "964XXXXXXXXXX@s.whatsapp.net"
}
```

Phone numbers are automatically formatted: leading `0` is stripped and Iraq country code `964` is prepended.

## Project Structure

```
├── Forms/
│   ├── MainForm          # Main window with data grid, scheduler, API settings
│   ├── MessageForm       # Dual-tab template editor with preview
│   ├── PhoneSenderForm   # Manual phone number sender
│   └── CredentialsForm   # SAS credential manager
├── Models/               # Data models (UserData, SasCredential, etc.)
├── Services/
│   ├── SasService        # SAS API integration
│   ├── WhatsAppService   # WhatsApp message sending
│   ├── CredentialManager # Encrypted credential storage
│   └── SASConnector      # Low-level SAS HTTP client
├── Helpers/
│   └── AESHelper         # AES encryption for credentials
└── Theme/                # Dark theme manager and renderer
```

## License

MIT
