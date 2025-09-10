namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.SolutionMetadata.Models
{
    internal class NugetPackageReference(string packageName, string packageVersion) : IEquatable<NugetPackageReference>
    {
        public string PackageName { get; } = packageName;

        public string PackageVersion { get; } = packageVersion;

        public bool Equals(NugetPackageReference? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return PackageName == other.PackageName && PackageVersion == other.PackageVersion;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((NugetPackageReference)obj);
        }

        public override int GetHashCode()
        {
            return PackageName.GetHashCode() + PackageVersion.GetHashCode();
        }
    }
}