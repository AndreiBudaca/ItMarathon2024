package org.tuiasi.engine.ui.uiWindows;

//import imgui.ImGui;
import imgui.ImFont;
import imgui.ImGuiStyle;
import imgui.flag.ImGuiDir;
import imgui.flag.ImGuiWindowFlags;
import imgui.internal.ImGui;
import imgui.ImVec2;
import imgui.internal.ImGuiDockNode;
import imgui.internal.flag.ImGuiDockNodeFlags;
import imgui.type.ImInt;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;
import org.tuiasi.engine.global.WindowVariables;
import org.tuiasi.engine.ui.components.IComponent;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@Getter @Setter
public class UIWindow extends IUIWindow{

    Integer dockspace_id;
    private Map<IUIWindow, DockData> dockedWindows = new HashMap<>();
    boolean isRoot = false;

    public UIWindow(String windowTitle) {
        super(windowTitle);
    }

    public UIWindow(String windowTitle, ImVec2 relativePosition, ImVec2 size){
        super(windowTitle, relativePosition, size);
    }

    public UIWindow(String windowTitle, int dockDirection, float dockRatio){
        super(windowTitle, dockDirection, dockRatio);
    }

    public UIWindow(String windowTitle, ImVec2 relativePosition, ImVec2 size, boolean isRootWindow){
        super(windowTitle, relativePosition, size);
        if(isRootWindow){
            addFlag(ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoDocking);
        }
        this.isRoot = isRootWindow;
    }

    public UIWindow(String windowTitle, int dockDirection, float dockRatio, boolean isRootWindow){
        super(windowTitle, dockDirection, dockRatio);
        if(isRootWindow){
            addFlag(ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoDocking);
        }
        this.isRoot = isRootWindow;
    }

    public void addDockedWindow(IUIWindow window, int direction, float splitRatio){
        dockedWindows.put(window, new DockData(direction, splitRatio));
    }

    @Override
    public void render() {
        super.render();


        // if the window is the root, autoresize to the window size
        if(isRoot) {
            WindowVariables windowVariables = WindowVariables.getInstance();

            ImVec2 windowSize = new ImVec2(windowVariables.getWidth(), windowVariables.getHeight() -windowVariables.getMainMenuHeight());
            ImGui.setNextWindowSize(windowSize.x, windowSize.y);
            ImGui.setNextWindowPos(windowVariables.getWindowPosX(), windowVariables.getWindowPosY() + windowVariables.getMainMenuHeight());
        }

        ImGui.begin(getWindowTitle(), getFlags());


        // render components inside
        for(int i = 0; i < getComponents().size(); ++ i){
            getComponents().get(i).render();
        }

        // create dockspace
        dockspace_id = ImGui.getID(getWindowTitle() + "_dockspace");

        ImGui.dockSpace(dockspace_id, 0, 0, ImGuiDockNodeFlags.PassthruCentralNode);

        // setup docked windows
        if(isFirstTime) {
            dockedSetup();
            configurePrefabComponents();
        }

        ImGui.end();

    }

    public void dockedSetup(){
        WindowVariables windowVariables = WindowVariables.getInstance();

        ImGui.dockBuilderRemoveNode(dockspace_id);
        ImGui.dockBuilderAddNode(dockspace_id, ImGuiDockNodeFlags.PassthruCentralNode | ImGuiDockNodeFlags.NoDockingInCentralNode | ImGuiDockNodeFlags.DockSpace);
        ImGui.dockBuilderSetNodeSize(dockspace_id, windowVariables.getWidth(), windowVariables.getHeight() - WindowVariables.getInstance().getMainMenuHeight());

        ImInt newNode = new ImInt(dockspace_id);
        ImInt oldNode = new ImInt();
        for(Map.Entry<IUIWindow, DockData> entry : dockedWindows.entrySet()){
            if(entry.getValue().getDirection() != ImGuiDir.None) {
                // split the node defined by the newNode id in 2 windows and return the id of the newly split node in the given direction
                int dock_id = ImGui.dockBuilderSplitNode(newNode.get(), entry.getValue().getDirection(), entry.getValue().getSplitRatio(), oldNode, newNode);

                // add the window defined by the windowLabel to the window with the id dock_id
                ImGui.dockBuilderDockWindow(entry.getKey().getWindowTitle(), oldNode.get());

                ImVec2 nodeSize = ImGui.dockBuilderGetNode(dock_id).getSize();
                entry.getKey().setSize(nodeSize);
            }else{
                // split the node defined by the newNode id in 2 windows and return the id of the newly split node in the given direction
                int dock_id = newNode.get();

                // add the window defined by the windowLabel to the window with the id dock_id
                ImGui.dockBuilderDockWindow(entry.getKey().getWindowTitle(), newNode.get());

                ImVec2 nodeSize = ImGui.dockBuilderGetNode(dock_id).getSize();
                entry.getKey().setSize(nodeSize);
            }

        }

        ImGui.dockBuilderFinish(dockspace_id);

        isFirstTime = false;
    }

    protected void addPrefabComponents(){}

    protected void configurePrefabComponents(){}
}
