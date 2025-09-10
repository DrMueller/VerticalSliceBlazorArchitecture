//namespace VerticalSliceBlazorArchitecture.Testing.Common.Fixtures
//{
//    public static class BenutzerContextTestBuilder
//    {
//        public static BenutzerContext BuildModanAdministrator()
//        {
//            return new BenutzerContext
//            {
//                ClientGroupCode = CodesProvider.ClientGroups.All.Value,
//                FarmRights = new BenutzerContextFarmRights
//                {
//                    CanManageBetrieb = false,
//                    CanManageTaetigkeiten = false,
//                    CanManageFarmCoreData = false
//                },
//                HasDatenschutzApproved = true,
//                AccountRights = new BenutzerContextAccountRights
//                {
//                    CanAdministrate = true,
//                    CanAssignClientGroup = true,
//                    CanImpersonate = false
//                }
//            };
//        }

//        public static BenutzerContext BuildInhaber(int betriebId)
//        {
//            return new BenutzerContext
//            {
//                BetriebId = betriebId,
//                ClientGroupCode = CodesProvider.ClientGroups.All.Value,
//                FarmRights = new BenutzerContextFarmRights
//                {
//                    CanManageBetrieb = true,
//                    CanManageTaetigkeiten = true,
//                    CanManageFarmCoreData = true
//                },
//                HasDatenschutzApproved = true,
//                AccountRights = new BenutzerContextAccountRights
//                {
//                    CanAdministrate = false,
//                    CanAssignClientGroup = false,
//                    CanImpersonate = false
//                }
//            };
//        }
//    }
//}

