
    public interface ISaveService
    {
        void SaveBool(string key, bool value);
        bool LoadBool(string key, bool defaultValue);
    }
