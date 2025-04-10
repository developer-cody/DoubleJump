using System.Text;
using BananaOS;
using BananaOS.Pages;

namespace DoubleJump.Page
{
    class DoubleJumpPage : WatchPage
    {
        private const string PageTitle = "<i><color=yellow>DoubleJump!</color><i>";

        public override bool DisplayOnMainMenu => true;
        public override string Title => PageTitle;
        public override void OnPostModSetup() => selectionHandler.maxIndex = 2;

        public override string OnGetScreenContent()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"<color=yellow>==</color> {PageTitle} <color=yellow>==</color>");
            sb.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, DoubleJumpPlugin._isEnabled ? "<color=red>Disable</color>" : "<color=green>Enable</color>"));
            sb.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(1, $"JumpForce: {DoubleJumpPlugin._jumpForce}"));
            sb.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(2, $"Cooldown: {DoubleJumpPlugin._jumpCooldown}"));
            return sb.ToString();
        }

        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            switch (buttonType)
            {
                case WatchButtonType.Up: selectionHandler.MoveSelectionUp(); break;
                case WatchButtonType.Down: selectionHandler.MoveSelectionDown(); break;
                case WatchButtonType.Right:
                    switch (selectionHandler.currentIndex)
                    {
                        case 1: DoubleJumpPlugin._jumpForce++; break;
                        case 2: DoubleJumpPlugin._jumpCooldown++; break;
                    }
                    break;
                case WatchButtonType.Left:
                    switch (selectionHandler.currentIndex)
                    {
                        case 1: DoubleJumpPlugin._jumpForce--; break;
                        case 2: DoubleJumpPlugin._jumpCooldown--; break;
                    }
                    break;
                case WatchButtonType.Enter:
                    if (selectionHandler.currentIndex == 0) DoubleJumpPlugin._isEnabled = !DoubleJumpPlugin._isEnabled;
                    break;
                case WatchButtonType.Back: ReturnToMainMenu(); break;
            }
        }
    }
}