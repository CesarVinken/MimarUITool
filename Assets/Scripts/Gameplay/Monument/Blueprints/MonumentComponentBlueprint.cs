
// The version of the monument component with all its original values, such as the base value of its costs and labour time.
// When we actually build a component we base it on this
public abstract class MonumentComponentBlueprint
{
    // TODO For each component, add list of Prerequisite blueprints: the components that should have been completed before this model is available
    public abstract int LabourTime { get; }
    public abstract string Name { get; }

    public abstract MonumentComponentBlueprint WithLabourTime(int labourTime);
    public abstract MonumentComponentBlueprint WithName(string name);
}
