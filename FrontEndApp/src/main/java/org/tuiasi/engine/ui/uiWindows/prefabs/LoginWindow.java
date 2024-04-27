package org.tuiasi.engine.ui.uiWindows.prefabs;

import imgui.ImGui;
import imgui.ImVec2;
import lombok.Getter;
import lombok.Setter;
import org.tuiasi.engine.ui.AppWindow;
import org.tuiasi.engine.ui.components.basicComponents.button.Button;
import org.tuiasi.engine.ui.components.basicComponents.button.ButtonListener;
import org.tuiasi.engine.ui.components.basicComponents.checkbox.CheckboxWithTitle;
import org.tuiasi.engine.ui.components.basicComponents.label.Label;
import org.tuiasi.engine.ui.components.basicComponents.searchbar.SearchbarWithHint;
import org.tuiasi.engine.ui.uiWindows.UIWindow;

import java.util.ArrayList;

public class LoginWindow extends UIWindow {

    @Getter @Setter
    private String mode = "Login";

    public LoginWindow(String windowTitle) {
        super(windowTitle);
    }

    public LoginWindow(String windowTitle, ImVec2 relativePosition, ImVec2 size) {
        super(windowTitle, relativePosition, size);
    }

    public LoginWindow(String windowTitle, int dockDirection, float dockRatio) {
        super(windowTitle, dockDirection, dockRatio);
    }

    public LoginWindow(String windowTitle, ImVec2 relativePosition, ImVec2 size, boolean isRootWindow) {
        super(windowTitle, relativePosition, size, isRootWindow);
    }

    public LoginWindow(String windowTitle, int dockDirection, float dockRatio, boolean isRootWindow) {
        super(windowTitle, dockDirection, dockRatio, isRootWindow);
        addPrefabComponents();
    }

    @Override
    protected void addPrefabComponents(){

        Label loginLabel = new Label(mode, false, 32);
        loginLabel.setRatioedPosition(0.5f, 0.2f);

        SearchbarWithHint emailInput = new SearchbarWithHint("Email", "Email", false);
        emailInput.setSize(300, 50);
        emailInput.setRatioedPosition(0.5f, 0.3f);

        SearchbarWithHint passwordInput = new SearchbarWithHint("Password", "Password", true);
        passwordInput.setSize(300, 50);
        passwordInput.setRatioedPosition(0.5f,  0.33f);

        Button loginButton = new Button(mode);
        loginButton.setSize(loginLabel.getWidth() + 100, 50);
        loginButton.setRatioedPosition(0.5f,  0.37f);

        Label switcherLabel = new Label(mode.equals("Login") ? "Nu ai cont? " : "Ai deja cont?", true, 16);
        switcherLabel.setRatioedPosition(0.5f, 0.45f);

        Button switcherButton = new Button(mode.equals("Login") ? "Inregistreaza-te" : "Logheaza-te");
        switcherButton.setSize(300, 40);
        switcherButton.setRatioedPosition(0.5f,  0.5f);
        switcherButton.setListener(new ButtonListener() {
            @Override
            public void onClick() {
                mode = mode.equals("Login") ? "Register" : "Login";
                updatePage();
            }
        });

        addComponent(loginLabel);
        addComponent(emailInput);
        addComponent(passwordInput);
        addComponent(loginButton);
        addComponent(switcherLabel);
        addComponent(switcherButton);
    }

    private void updatePage(){
        setComponents(new ArrayList<>());
        addPrefabComponents();
    }

    @Override
    protected void configurePrefabComponents(){
    }
}
