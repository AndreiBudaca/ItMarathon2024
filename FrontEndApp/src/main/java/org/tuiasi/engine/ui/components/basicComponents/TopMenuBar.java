package org.tuiasi.engine.ui.components.basicComponents;

import imgui.flag.ImGuiStyleVar;
import imgui.internal.ImGui;
import lombok.Getter;
import org.tuiasi.engine.global.WindowVariables;
import org.tuiasi.engine.networking.APICaller;
import org.tuiasi.engine.ui.DefaultAppUI;
import org.tuiasi.engine.ui.components.IComponent;

public class TopMenuBar extends IComponent {

    public String getLabel() {
        return "TopMenuBar";
    }
    public void setLabel(String label){}

    @Override
    public void render() {

        // top menu bar with 2 options: file and edit

        ImGui.pushStyleVar(ImGuiStyleVar.FramePadding, 0, 10);
        ImGui.beginMainMenuBar();

        WindowVariables.getInstance().setMainMenuHeight(ImGui.getWindowSize().y);

        if (ImGui.beginMenu("Options")) {
            if(APICaller.current_jwt != null && !APICaller.current_jwt.equals("The email / password combination is invalid"))
                if(ImGui.menuItem("Logout")){
                    DefaultAppUI.setCurrentPage("LoginPage");
                    APICaller.current_jwt = null;
                    DefaultAppUI.removePage("StudentHomeWindow");
                }
            ImGui.endMenu();
        }

        ImGui.endMainMenuBar();
        ImGui.popStyleVar();
    }
}
