﻿using System.Diagnostics.CodeAnalysis;

namespace No8.Areaz.Console;

/// <summary>
/// Just like ConsoleKey, but better
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Because")]
public enum VirtualKeyCode : ushort
{
    LBUTTON    = 0x01, // Left mouse button
    RBUTTON    = 0x02, // Right mouse button
    CANCEL     = 0x03, // Control-break processing
    MBUTTON    = 0x04, // Middle mouse button (three-button mouse)
    XBUTTON1   = 0x05, // X1 mouse button
    XBUTTON2   = 0x06, // X2 mouse button
    UNDEFINED1 = 0x07, // Undefined
    BACK       = 0x08, // BACKSPACE key
    TAB        = 0x09, // TAB key
    UNDEFINED2 = 0x0A, //Reserved
    UNDEFINED3 = 0x0B, //Reserved
    CLEAR      = 0x0C, // CLEAR key
    RETURN     = 0x0D, // ENTER key
    UNDEFINED4 = 0x0E, // Undefined
    UNDEFINED5 = 0x0F, // Undefined

    SHIFT      = 0x10, // SHIFT key
    CONTROL    = 0x11, // CTRL key
    MENU       = 0x12, // ALT key
    PAUSE      = 0x13, // PAUSE key
    CAPITAL    = 0x14, //CAPS LOCK key
    KANA       = 0x15, //IME Kana mode
    HANGUEL    = 0x15, //IME Hanguel mode (maintained for compatibility; use VK_HANGUL)
    HANGUL     = 0x15, // IME Hangul mode
    IME_ON     = 0x16, // IME On
    JUNJA      = 0x17, // IME Junja mode
    FINAL      = 0x18, // IME final mode
    HANJA      = 0x19, // IME Hanja mode
    KANJI      = 0x19, // IME Kanji mode
    IME_OFF    = 0x1A, // IME Off
    ESCAPE     = 0x1B, // ESC key
    CONVERT    = 0x1C, // IME convert
    NONCONVERT = 0x1D, // IME nonconvert
    ACCEPT     = 0x1E, // IME accept
    MODECHANGE = 0x1F, // IME mode change request

    SPACE    = 0x20, // SPACEBAR
    PRIOR    = 0x21, // PAGE UP key
    NEXT     = 0x22, // PAGE DOWN key
    END      = 0x23, // END key
    HOME     = 0x24, // HOME key
    LEFT     = 0x25, // LEFT ARROW key
    UP       = 0x26, // UP ARROW key
    RIGHT    = 0x27, // RIGHT ARROW key
    DOWN     = 0x28, // DOWN ARROW key
    SELECT   = 0x29, // SELECT key
    PRINT    = 0x2A, // PRINT key
    EXECUTE  = 0x2B, // EXECUTE key
    SNAPSHOT = 0x2C, // PRINT SCREEN key
    INSERT   = 0x2D, // INS key
    DELETE   = 0x2E, // DEL key
    HELP     = 0x2F, // HELP key

    VK_0 = 0x30, // 0 key
    VK_1 = 0x31, // 1 KEY
    VK_2 = 0x32, // 2 KEY
    VK_3 = 0x33, // 3 KEY
    VK_4 = 0x34, // 4 KEY
    VK_5 = 0x35, // 5 KEY
    VK_6 = 0x36, // 6 KEY
    VK_7 = 0x37, // 7 KEY
    VK_8 = 0x38, // 8 KEY
    VK_9 = 0x39, // 9 KEY   - 0x3A-40 Undefined

    VK_A = 0x41, // A key
    VK_B = 0x42, // B key
    VK_C = 0x43, // C key
    VK_D = 0x44, // D key
    VK_E = 0x45, // E key
    VK_F = 0x46, // F key
    VK_G = 0x47, // G key
    VK_H = 0x48, // H key
    VK_I = 0x49, // I key
    VK_J = 0x4A, // J key
    VK_K = 0x4B, // K key
    VK_L = 0x4C, // L key
    VK_M = 0x4D, // M key
    VK_N = 0x4E, // N key
    VK_O = 0x4F, // O key

    VK_P          = 0x50, // P key
    VK_Q          = 0x51, // Q key
    VK_R          = 0x52, // R key
    VK_S          = 0x53, // S key
    VK_T          = 0x54, // T key
    VK_U          = 0x55, // U key
    VK_V          = 0x56, // V key
    VK_W          = 0x57, // W key
    VK_X          = 0x58, // X key
    VK_Y          = 0x59, // Y key
    VK_Z          = 0x5A, // Z key
    LWIN       = 0x5B, // Left Windows key (Natural keyboard)
    RWIN       = 0x5C, // Right Windows key (Natural keyboard)
    APPS       = 0x5D, // Applications key (Natural keyboard)
    UNDEFINED6 = 0x5E, // Reserved
    SLEEP      = 0x5F, // Computer Sleep key

    NUMPAD0   = 0x60, // Numeric keypad 0 key
    NUMPAD1   = 0x61, // Numeric keypad 1 key
    NUMPAD2   = 0x62, // Numeric keypad 2 key
    NUMPAD3   = 0x63, // Numeric keypad 3 key
    NUMPAD4   = 0x64, // Numeric keypad 4 key
    NUMPAD5   = 0x65, // Numeric keypad 5 key
    NUMPAD6   = 0x66, // Numeric keypad 6 key
    NUMPAD7   = 0x67, // Numeric keypad 7 key
    NUMPAD8   = 0x68, // Numeric keypad 8 key
    NUMPAD9   = 0x69, // Numeric keypad 9 key
    MULTIPLY  = 0x6A, // Multiply key
    ADD       = 0x6B, // Add key
    SEPARATOR = 0x6C, // Separator key
    SUBTRACT  = 0x6D, // Subtract key
    DECIMAL   = 0x6E, // Decimal key
    DIVIDE    = 0x6F, // Divide key

    F1  = 0x70, // F1 key
    F2  = 0x71, // F2 key
    F3  = 0x72, // F3 key
    F4  = 0x73, // F4 key
    F5  = 0x74, // F5 key
    F6  = 0x75, // F6 key
    F7  = 0x76, // F7 key
    F8  = 0x77, // F8 key
    F9  = 0x78, // F9 key
    F10 = 0x79, // F10 key
    F11 = 0x7A, // F11 key
    F12 = 0x7B, // F12 key
    F13 = 0x7C, // F13 key
    F14 = 0x7D, // F14 key
    F15 = 0x7E, // F15 key
    F16 = 0x7F, // F16 key

    F17 = 0x80, // F17 key
    F18 = 0x81, // F18 key
    F19 = 0x82, // F19 key
    F20 = 0x83, // F20 key
    F21 = 0x84, // F21 key
    F22 = 0x85, // F22 key
    F23 = 0x86, // F23 key
    F24 = 0x87, // F24 key = 0x88-8F,           // Unassigned

    NUMLOCK = 0x90, // NUM LOCK key
    SCROLL  = 0x91, // SCROLL LOCK key
    // = 0x92-96, OEM specific
    // = 0x97-9F, Unassigned

    LSHIFT            = 0xA0, // Left SHIFT key
    RSHIFT            = 0xA1, // Right SHIFT key
    LCONTROL          = 0xA2, // Left CONTROL key
    RCONTROL          = 0xA3, // Right CONTROL key
    LMENU             = 0xA4, // Left MENU key
    RMENU             = 0xA5, // Right MENU key
    BROWSER_BACK      = 0xA6, // Browser Back key
    BROWSER_FORWARD   = 0xA7, // Browser Forward key
    BROWSER_REFRESH   = 0xA8, // Browser Refresh key
    BROWSER_STOP      = 0xA9, // Browser Stop key
    BROWSER_SEARCH    = 0xAA, // Browser Search key
    BROWSER_FAVORITES = 0xAB, // Browser Favorites key
    BROWSER_HOME      = 0xAC, // Browser Start and Home key
    VOLUME_MUTE       = 0xAD, // Volume Mute key
    VOLUME_DOWN       = 0xAE, // Volume Down key
    VOLUME_UP         = 0xAF, // Volume Up key

    MEDIA_NEXT_TRACK    = 0xB0, // Next Track key
    MEDIA_PREV_TRACK    = 0xB1, // Previous Track key
    MEDIA_STOP          = 0xB2, // Stop Media key
    MEDIA_PLAY_PAUSE    = 0xB3, // Play/Pause Media key
    LAUNCH_MAIL         = 0xB4, // Start Mail key
    LAUNCH_MEDIA_SELECT = 0xB5, // Select Media key
    LAUNCH_APP1         = 0xB6, // Start Application 1 key
    LAUNCH_APP2         = 0xB7, // Start Application 2 key
    // - 0xB8-B9, //  Reserved
    OEM_1 = 0xBA, // Used for miscellaneous characters; it can vary by keyboard.
    // For the US standard keyboard, the ';:' key
    OEM_PLUS   = 0xBB, // For any country/region, the '+' key
    OEM_COMMA  = 0xBC, // For any country/region, the ',' key
    OEM_MINUS  = 0xBD, // For any country/region, the '-' key
    OEM_PERIOD = 0xBE, // For any country/region, the '.' key
    OEM_2      = 0xBF, // Used for miscellaneous characters; it can vary by keyboard.

    // For the US standard keyboard, the '/?' key
    OEM_3 = 0xC0, // Used for miscellaneous characters; it can vary by keyboard.
    // For the US standard keyboard, the '`~' key
    // =0xC1-D7, // Reserved

    // =0xD8-DA, // Unassigned
    OEM_4 = 0xDB, // Used for miscellaneous characters; it can vary by keyboard.
    OEM_5 = 0xDC, // For the US standard keyboard, the '[{' key
    OEM_6 = 0xDD, // For the US standard keyboard, the '\|' key
    OEM_7 = 0xDE, // For the US standard keyboard, the ']}' key
    OEM_8 = 0xDF, // For the US standard keyboard, the 'single-quote/double-quote' key

    // =0xE0, // Reserved
    // =0xE1, // OEM specific
    OEM_102 = 0xE2, // Either the angle bracket key or the backslash key on the RT 102-key keyboard
    // =0xE3-E4, // OEM specific
    PROCESSKEY = 0xE5, // IME PROCESS key
    // =0xE6, // OEM specific
    PACKET = 0xE7, // Used to pass Unicode characters as if they were keystrokes.
    // The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods.For more

    // information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP
    // =-0xE8,    // Unassigned
    // =0xE9-F5, // OEM specific

    ATTN      = 0xF6, // Attn key
    CRSEL     = 0xF7, // CrSel key
    EXSEL     = 0xF8, // ExSel key
    EREOF     = 0xF9, // Erase EOF key
    PLAY      = 0xFA, // Play key
    ZOOM      = 0xFB, // Zoom key
    NONAME    = 0xFC, // Reserved
    PA1       = 0xFD, // PA1 key
    OEM_CLEAR = 0xFE, // Clear k
}