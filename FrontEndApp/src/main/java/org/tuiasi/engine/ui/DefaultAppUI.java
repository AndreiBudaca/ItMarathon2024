package org.tuiasi.engine.ui;

import imgui.*;
import imgui.flag.ImGuiDir;
import lombok.Getter;
import lombok.Setter;
import org.tuiasi.engine.ui.components.basicComponents.TopMenuBar;
import org.tuiasi.engine.ui.components.basicComponents.button.Button;
import org.tuiasi.engine.ui.uiWindows.IUIWindow;
import org.tuiasi.engine.ui.uiWindows.Page;
import org.tuiasi.engine.ui.uiWindows.UIWindow;
import org.tuiasi.engine.ui.uiWindows.prefabs.LoginWindow;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

@Getter @Setter
public class DefaultAppUI {
    private UIWindow mainWindow;

    private TopMenuBar topMenuBar;
    private HashMap<String, Page> pages;
    private Page currentPage;
    boolean isSetup = false;

    public DefaultAppUI() {
        topMenuBar = new TopMenuBar();
        pages = new HashMap<>();

        // create all pages in application
        Page loginPage = new Page("LoginPage",
                List.of(
                            new LoginWindow("Login Window", ImGuiDir.None, 1.0f, true)
                        )
        );
        pages.put(loginPage.getName(), loginPage);

        // set current page
        currentPage = pages.get("LoginPage");

        // initialize main window and add all docked windows from the current page
        mainWindow = new UIWindow("Main Window", new ImVec2(0, 0), null, true);
        mainWindow.setDocked(true);
        for(IUIWindow window: currentPage.getIuiWindows()){
            mainWindow.addDockedWindow(window, window.getDockPosition(), window.getDockRatio());
        }

    }

    public void renderUI() {
        topMenuBar.render();

        mainWindow.render();
        for (IUIWindow uiWindow : currentPage.getIuiWindows()) {
            uiWindow.render();
        }

    }

}
