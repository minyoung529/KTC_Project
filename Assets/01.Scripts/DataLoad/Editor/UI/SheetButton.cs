using UnityEngine.UIElements;

public class SheetButton
{
    private TemplateContainer container;
    private SheetInformation spreadInfo;
    private Button button;

    public int Index => spreadInfo.index;
    public TemplateContainer Container => container;
    public Button Button => button;

    public void Initialize(SheetInformation info, TemplateContainer container)
    {
        spreadInfo = info;
        this.container = container;

        button = container.Q<Button>("Button");
        SetText(spreadInfo.sheetName);

        // UnityEngine.RigidbodyConstraints
    }

    public void SetText(string text)
    {
        button.text = $"{Index}. {text}";
    }
}