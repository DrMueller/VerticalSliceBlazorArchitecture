//using Moq;

//namespace VerticalSliceBlazorArchitecture.Testing.Common.Fixtures
//{
//    public class BenutzerContextStateMock
//    {
//        private readonly Mock<IBenutzerContextState> _mock;

//        private BenutzerContextStateMock(BenutzerContext context)
//        {
//            _mock = new Mock<IBenutzerContextState>();
//            _mock.Setup(f => f.CheckBenutzerExistsAsync()).ReturnsAsync(true);
//            _mock.Setup(f => f.GetBenutzerAsync()).ReturnsAsync(context);
//        }

//        public IBenutzerContextState Object => _mock.Object;

//        public static BenutzerContextStateMock CreateForInhaber(int betriebId)
//        {
//            return new BenutzerContextStateMock(BenutzerContextTestBuilder.BuildInhaber(betriebId));
//        }
//    }
//}

