﻿using Content.Client.Chat;
using Content.Client.Interfaces;
using Robust.Client.AutoGenerated;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.GameObjects;
using Robust.Shared.Maths;

namespace Content.Client.UserInterface
{
    [GenerateTypedNameReferences]
    internal sealed partial class LobbyGui : Control
    {
        public Label ServerName => CServerName;
        public Label StartTime => CStartTime;
        public Button ReadyButton => CReadyButton;
        public Button ObserveButton => CObserveButton;
        public Button OptionsButton => COptionsButton;
        public Button LeaveButton => CLeaveButton;
        public ChatBox Chat => CChat;
        public VBoxContainer VoteContainer => CVoteContainer;
        public LobbyPlayerList OnlinePlayerList => COnlinePlayerList;
        public ServerInfo ServerInfo => CServerInfo;
        public LobbyCharacterPreviewPanel CharacterPreview { get; }

        public LobbyGui(IEntityManager entityManager,
            IClientPreferencesManager preferencesManager)
        {
            RobustXamlLoader.Load(this);

            ServerName.HorizontalExpand = true;
            ServerName.HorizontalAlignment = HAlignment.Center;

            CharacterPreview = new LobbyCharacterPreviewPanel(
                entityManager,
                preferencesManager)
            {
                HorizontalAlignment = HAlignment.Left
            };

            CLeftPanelContainer.AddChild(CharacterPreview);
            CharacterPreview.SetPositionFirst();
        }
    }

    public class LobbyPlayerList : Control
    {
        private readonly ScrollContainer _scroll;
        private readonly VBoxContainer _vBox;

        public LobbyPlayerList()
        {
            var panel = new PanelContainer()
            {
                PanelOverride = new StyleBoxFlat {BackgroundColor = Color.FromHex("#202028")},
            };
            _vBox = new VBoxContainer();
            _scroll = new ScrollContainer();
            _scroll.AddChild(_vBox);
            panel.AddChild(_scroll);
            AddChild(panel);
        }

        // Adds a row
        public void AddItem(string name, string status)
        {
            var hbox = new HBoxContainer
            {
                HorizontalExpand = true,
            };

            // Player Name
            hbox.AddChild(new PanelContainer()
            {
                PanelOverride = new StyleBoxFlat
                {
                    BackgroundColor = Color.FromHex("#373744"),
                    ContentMarginBottomOverride = 2,
                    ContentMarginLeftOverride = 4,
                    ContentMarginRightOverride = 4,
                    ContentMarginTopOverride = 2
                },
                Children =
                {
                    new Label
                    {
                        Text = name
                    }
                },
                HorizontalExpand = true
            });
            // Status
            hbox.AddChild(new PanelContainer()
            {
                PanelOverride = new StyleBoxFlat
                {
                    BackgroundColor = Color.FromHex("#373744"),
                    ContentMarginBottomOverride = 2,
                    ContentMarginLeftOverride = 4,
                    ContentMarginRightOverride = 4,
                    ContentMarginTopOverride = 2
                },
                Children =
                {
                    new Label
                    {
                        Text = status
                    }
                },
                HorizontalExpand = true,
                SizeFlagsStretchRatio = 0.2f,
            });

            _vBox.AddChild(hbox);
        }

        // Deletes all rows
        public void Clear()
        {
            _vBox.RemoveAllChildren();
        }
    }
}