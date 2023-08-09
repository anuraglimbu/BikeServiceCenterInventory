using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeServiceCenterInventory.Models;

public class BaseRecordView : Record
{
#nullable enable
	public string? ItemName { get; set; }
#nullable disable
}
