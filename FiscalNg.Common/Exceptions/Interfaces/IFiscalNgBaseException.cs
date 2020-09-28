using FiscalNg.Common.Exceptions.Enums;

namespace FiscalNg.Common.Exceptions.Interfaces {
   public interface IFiscalNgBaseException {
       ErrorCode Result { get; }
    }
}
