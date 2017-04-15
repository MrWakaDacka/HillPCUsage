namespace HILPcUsage
{
    class ProcessInfo
    {
        public string Name { get; set; }
        public float CPUUsage { get; set; }
        public float RAMUsage { get; set; }

        public ProcessInfo() { }

        public ProcessInfo(string Name, float RAMUsage, float MemoryUsage) {

            this.Name = Name;
            this.CPUUsage = RAMUsage;
            this.RAMUsage = MemoryUsage;
        }
    }
}
