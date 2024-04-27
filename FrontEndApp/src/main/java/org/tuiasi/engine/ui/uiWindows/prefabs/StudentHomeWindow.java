package org.tuiasi.engine.ui.uiWindows.prefabs;

import imgui.ImVec2;
import imgui.flag.ImGuiDir;
import imgui.flag.ImGuiWindowFlags;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.tuiasi.engine.networking.*;
import org.tuiasi.engine.ui.components.basicComponents.button.Button;
import org.tuiasi.engine.ui.components.basicComponents.button.ButtonListener;
import org.tuiasi.engine.ui.components.basicComponents.dropdown.DropdownListener;
import org.tuiasi.engine.ui.components.basicComponents.dropdown.DropdownWithTitle;
import org.tuiasi.engine.ui.components.basicComponents.label.Label;
import org.tuiasi.engine.ui.uiWindows.IUIWindow;
import org.tuiasi.engine.ui.uiWindows.UIWindow;

import java.util.ArrayList;
import java.util.List;

public class StudentHomeWindow  extends UIWindow {

    private OptionalPacksDTO optionalPacks;
    private UIWindow optionalHalfWindow;
    private Integer[][] packsWithPreferences;

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

        APICaller apiCaller = new APICaller();
        optionalPacks = new OptionalPacksDTO();

        JSONArray obj = (JSONArray) apiCaller.getOptionalPacks();

        if (obj != null) {
            // iterate packs
            for (int i = 0; i < obj.size(); i++) {
                // get pack
                JSONObject pack = (JSONObject) obj.get(i);
                OptionalPackDTO optionalPack = new OptionalPackDTO();

                // iterate optionals in each pack
                JSONArray options = (JSONArray) pack.get("options");
                for (int j = 0; j < options.size(); j++) {
                    // get option
                    JSONObject option = (JSONObject) options.get(j);

                    // add option to DTO
                    Long preferenceIndex = ((Long) option.get("sortOrder"));
                    OptionalDTO optional = new OptionalDTO(Math.toIntExact((Long) option.get("id")), (String) option.get("name"), (String) option.get("description"), preferenceIndex == null ? 0 : preferenceIndex.intValue());
                    optionalPack.getOptionals().add(optional);
                }

                // add the optional pack to the optionalPacksDTO
                optionalPacks.getOptionalPacks().add(optionalPack);
            }
        }

        addPrefabComponents();
    }


    @Override
    protected void addPrefabComponents(){

        if(optionalPacks != null) {

            Label explanationLabel = new Label("Salut! De aici iti poti alege preferintele.\nAlege pentru fiecare pachet optiunile in ordinea dorita.", false, 32);
            explanationLabel.setRatioedPosition(0.5f, 0.05f);
            addComponent(explanationLabel);

            Label saveResultLabel = new Label("");
            saveResultLabel.setRatioedPosition(0.5f, 0.3f);
            saveResultLabel.setFontSize(18);
            addComponent(saveResultLabel);

            Button saveButton = new Button("Salveaza preferintele");
            saveButton.setRatioedPosition(0.5f, 0.2f);
            saveButton.setSize(300, 100);
            saveButton.setListener(new ButtonListener() {
                @Override
                public void onClick() {
                    // se creaza perechi optional-preferinta
                    List<SubjectPreferenceDTO> preference_order_pair = new ArrayList<>();
                    for (int i = 0; i < packsWithPreferences.length; i++) {
                        for (int j = 0; j < packsWithPreferences[i].length; j++) {
                            preference_order_pair.add(new SubjectPreferenceDTO(packsWithPreferences[i][j], j));
                        }
                    }
                    APICaller apiCaller = new APICaller();
                    boolean status = apiCaller.saveOptionals(preference_order_pair);
                    saveResultLabel.setLabel(status ? "Preferintele au fost salvate cu succes!" : "A aparut o eroare. Verifica optiunile");
                }
            });
            addComponent(saveButton);


            optionalHalfWindow = new UIWindow("Optionale", ImGuiDir.Down, 0.6f);
            optionalHalfWindow.setDocked(true);
            optionalHalfWindow.addFlag(ImGuiWindowFlags.NoMove);
            addDockedWindow(optionalHalfWindow, optionalHalfWindow.getDockPosition(), optionalHalfWindow.getDockRatio());

            int packCount = optionalPacks.getOptionalPacks().size();
            packsWithPreferences = new Integer[packCount][];

            // iterare pachete
            for (int i = 0; i < packCount; i++) {

                UIWindow packWindow = new UIWindow("Pachet " + (i + 1), i == packCount - 1 ? ImGuiDir.None : ImGuiDir.Left, 1.0f / (packCount - i));

                packWindow.setDocked(true);
                packWindow.addFlag(ImGuiWindowFlags.NoMove);

                List<OptionalDTO> optionals = optionalPacks.getOptionalPacks().get(i).getOptionals();
                int optionalInPackCount = optionals.size();
                packsWithPreferences[i] = new Integer[optionalInPackCount];


                Integer[] optionalIds = new Integer[optionalInPackCount];
                String[] optionalNames = new String[optionalInPackCount];
                String[] optionalDescriptions = new String[optionalInPackCount];
                Integer[] optionalPreferences = new Integer[optionalInPackCount];
                for (int j = 0; j < optionalInPackCount; ++j) {
                    optionalIds[j] = optionals.get(j).getId();
                    optionalNames[j] = optionals.get(j).getName();
                    optionalDescriptions[j] = optionals.get(j).getDescription();
                    optionalPreferences[j] = optionals.get(j).getPreferenceIndex();
                }
                // creaza dropdownuri pt cate optionale sunt in pachet
                for (int j = 0; j < optionalInPackCount; ++j) {
                    int finalI = i;
                    int finalJ = j;
                    DropdownWithTitle optionalPack = new DropdownWithTitle("Optiunea " + (j + 1), optionalNames, optionalDescriptions, optionalPreferences[j], new DropdownListener() {
                        @Override
                        public void onItemSelected(int index) {
                            packsWithPreferences[finalI][finalJ] = optionalIds[index];
                        }
                    });

                    optionalPack.setXRatioToWindow(.6f);
                    optionalPack.setRatioedPosition(0.4f, 0.2f + 0.15f * j);
                    packWindow.addComponent(optionalPack);
                }

                optionalHalfWindow.addDockedWindow(packWindow, packWindow.getDockPosition(), packWindow.getDockRatio());
            }
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