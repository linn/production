namespace Linn.Production.Domain.LinnApps
{
    public class ManufacturingOperation
    {
        public ManufacturingOperation(
            string routeCode,
            int manufacturingId,
            int operationNumber,
            string description,
            string skillCode,
            string resourceCode,
            int setAndCleanTime,
            int cycleTime,
            int labourPercentage,
            string citCode)
        {
            this.RouteCode = routeCode;
            this.ManufacturingId = manufacturingId;
            this.OperationNumber = operationNumber;
            this.Description = description;
            this.SkillCode = skillCode;
            this.ResourceCode = resourceCode;
            this.SetAndCleanTime = setAndCleanTime;
            this.CycleTime = cycleTime;
            this.LabourPercentage = labourPercentage;
            this.CITCode = citCode;
        }

        private ManufacturingOperation()
        {
        }

        public string RouteCode { get; set; }

        public int ManufacturingId { get; set; }

        public int OperationNumber { get; set; }

        public string Description { get; set; }

        public string SkillCode { get; set; }

        public string ResourceCode { get; set; }

        public int SetAndCleanTime { get; set; }

        public int CycleTime { get; set; }

        public int LabourPercentage { get; set; }

        public string CITCode { get; set; }

        public ManufacturingRoute ManufacturingRoute { get; set; }
    }
}
