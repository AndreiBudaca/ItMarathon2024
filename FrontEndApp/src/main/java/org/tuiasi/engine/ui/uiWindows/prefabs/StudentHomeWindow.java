package org.tuiasi.engine.ui.uiWindows.prefabs;

import imgui.ImVec2;
import imgui.flag.ImGuiDir;
import imgui.flag.ImGuiDockNodeFlags;
import imgui.flag.ImGuiWindowFlags;
import lombok.Getter;
import lombok.Setter;
import org.tuiasi.engine.networking.APICaller;
import org.tuiasi.engine.networking.OptionalPacksDTO;
import org.tuiasi.engine.ui.components.basicComponents.button.Button;
import org.tuiasi.engine.ui.components.basicComponents.button.ButtonListener;
import org.tuiasi.engine.ui.components.basicComponents.dropdown.DropdownWithTitle;
import org.tuiasi.engine.ui.components.basicComponents.label.Label;
import org.tuiasi.engine.ui.uiWindows.IUIWindow;
import org.tuiasi.engine.ui.uiWindows.UIWindow;

import java.util.List;

public class StudentHomeWindow  extends UIWindow {

    private OptionalPacksDTO optionalPacks;
    private UIWindow optionalHalfWindow;

    public StudentHomeWindow(String windowTitle) {
        super(windowTitle);
    }

    public StudentHomeWindow(String windowTitle, ImVec2 relativePosition, ImVec2 size) {
        super(windowTitle, relativePosition, size);
    }

    public StudentHomeWindow(String windowTitle, int dockDirection, float dockRatio) {
        super(windowTitle, dockDirection, dockRatio);
    }

    public StudentHomeWindow(String windowTitle, ImVec2 relativePosition, ImVec2 size, boolean isRootWindow) {
        super(windowTitle, relativePosition, size, isRootWindow);
    }

    public StudentHomeWindow(String windowTitle, int dockDirection, float dockRatio, boolean isRootWindow) {
        super(windowTitle, dockDirection, dockRatio, isRootWindow);
        addPrefabComponents();

        APICaller apiCaller = new APICaller();
        // TODO: Map api response to optionalPacksDTO
//        apiCaller.getOptionalPacks();
    }


    @Override
    protected void addPrefabComponents(){
        Label explanationLabel = new Label("Salut! De aici iti poti alege preferintele.\nAlege pentru fiecare pachet optiunile in ordinea dorita.", false, 32);
        explanationLabel.setRatioedPosition(0.5f, 0.05f);
        addComponent(explanationLabel);

        Label saveResultLabel = new Label("Lorem ipsum");
        saveResultLabel.setRatioedPosition(0.5f, 0.3f);
        saveResultLabel.setFontSize(18);
        addComponent(saveResultLabel);

        Button saveButton = new Button("Salveaza preferintele");
        saveButton.setRatioedPosition(0.5f, 0.2f);
        saveButton.setSize(300, 100);
        saveButton.setListener(new ButtonListener() {
            @Override
            public void onClick() {
                // TODO: Send preferences and post response status
            }
        });
        addComponent(saveButton);


        optionalHalfWindow = new UIWindow("Optionale", ImGuiDir.Down, 0.6f);
        optionalHalfWindow.setDocked(true);
        optionalHalfWindow.addFlag(ImGuiWindowFlags.NoMove);
        addDockedWindow(optionalHalfWindow, optionalHalfWindow.getDockPosition(), optionalHalfWindow.getDockRatio());

        int packCount = 3;
        int optionalInPackCount = 3;

        // iterare pachete
        for(int i = 0; i < packCount; i++){

            UIWindow packWindow = new UIWindow("Pachet " + i, i == packCount - 1 ? ImGuiDir.None : ImGuiDir.Left, 1.0f / (packCount-i));

            packWindow.setDocked(true);
            packWindow.addFlag(ImGuiWindowFlags.NoMove);

            // iterare optionale din pachete
            for(int j = 0; j < optionalInPackCount; ++ j) {
                DropdownWithTitle optionalPack = new DropdownWithTitle("Optiunea " + j, new String[]{"APD", "PBD", "PC2"});
                optionalPack.setXRatioToWindow(.6f);
//                optionalPack.setSize(200, 50);
                optionalPack.setRatioedPosition(0.4f, 0.1f * j);
                packWindow.addComponent(optionalPack);
            }

            optionalHalfWindow.addDockedWindow(packWindow, packWindow.getDockPosition(), packWindow.getDockRatio());
        }
    }

    @Override
    public void render() {
        super.render();

        optionalHalfWindow.render();
        for(IUIWindow packWindow: optionalHalfWindow.getDockedWindows().keySet())
            packWindow.render();
    }

    @Override
    protected void configurePrefabComponents(){
    }
}