package org.tuiasi.engine.ui.uiWindows.prefabs;

import imgui.ImVec2;
import org.tuiasi.engine.ui.components.basicComponents.button.Button;
import org.tuiasi.engine.ui.components.basicComponents.button.ButtonListener;
import org.tuiasi.engine.ui.uiWindows.UIWindow;

public class AdminHomeWindow extends UIWindow {

    public AdminHomeWindow(String windowTitle) {
        super(windowTitle);
    }

    public AdminHomeWindow(String windowTitle, ImVec2 relativePosition, ImVec2 size) {
        super(windowTitle, relativePosition, size);
    }

    public AdminHomeWindow(String windowTitle, int dockDirection, float dockRatio) {
        super(windowTitle, dockDirection, dockRatio);
    }

    public AdminHomeWindow(String windowTitle, ImVec2 relativePosition, ImVec2 size, boolean isRootWindow) {
        super(windowTitle, relativePosition, size, isRootWindow);
    }

    public AdminHomeWindow(String windowTitle, int dockDirection, float dockRatio, boolean isRootWindow) {
        super(windowTitle, dockDirection, dockRatio, isRootWindow);
        addPrefabComponents();
    }


    @Override
    protected void addPrefabComponents(){
        Button stopSelectionButton = new Button("Click here to stop selections", new ButtonListener() {
            @Override
            public void onClick() {

            }
        });
        addComponent(stopSelectionButton);
    }

    @Override
    protected void configurePrefabComponents(){
    }
}
