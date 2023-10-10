using UnityEngine.UIElements;

public class SpreadManageButton
{
    private TemplateContainer container;
    private SpreadInformation spreadInfo;
    private Button button;

    public int Index => spreadInfo.index;
    public TemplateContainer Container => container;
    public Button Button => button;

    public void Initialize(SpreadInformation info, TemplateContainer container)
    {
        spreadInfo = info;
        this.container = container;

        button = container.Q<Button>("Button");
        SetText(spreadInfo.sheetName);
    }

    public void SetText(string text)
    {
        button.text = $"{Index}. {text}";
    }
}