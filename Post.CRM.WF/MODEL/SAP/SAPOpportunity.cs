using System.Collections.Generic;
using System.Xml.Serialization;

namespace Post.CRM.WF.MODEL.SAP.Opportunities
{
	[XmlType("EDI_DC40")]
	public class EDI_DC40
	{
		[XmlElement(ElementName = "DOCNUM")]

		public string DOCNUM { get; set; }
		[XmlElement(ElementName = "IDOCTYP")]


		public string IDOCTYP { get; set; }
		[XmlElement(ElementName = "MESTYP")]

		public string MESTYP { get; set; }
		[XmlElement(ElementName = "SNDPOR")]

		public string SNDPOR { get; set; }
		[XmlElement(ElementName = "SNDPRT")]

		public string SNDPRT { get; set; }
		[XmlElement(ElementName = "SNDPRN")]

		public string SNDPRN { get; set; }
		[XmlElement(ElementName = "RCVPOR")]

		public string RCVPOR { get; set; }
		[XmlElement(ElementName = "RCVPRT")]

		public string RCVPRT { get; set; }
		[XmlElement(ElementName = "RCVPRN")]

		public string RCVPRN { get; set; }

		[XmlAttribute("SEGMENT")]
		public string SEGMENT;
	}
	
	[XmlType("E1BPSDHD1")]
	public class E1BPSDHD1
	{
		public string DOC_TYPE { get; set; }
		public string SALES_ORG { get; set; }
	
		public string DISTR_CHAN { get; set; }
		public string DIVISION { get; set; }
		public string SALES_OFF { get; set; }
		public string REQ_DATE_H { get; set; }
		
		public string NAME { get; set; }
		public string PURCH_NO_C { get; set; }
		public string CURRENCY { get; set; }
		[XmlAttribute("SEGMENT")]
		public string SEGMENT { get; set; }
	}

	[XmlType("E1BPSDHD1X")]
	public class E1BPSDHD1X
	{
		public string UPDATEFLAG { get; set; }

		public string DOC_TYPE { get; set; }

		public string SALES_ORG { get; set; }

		public string DISTR_CHAN { get; set; }

		public string DIVISION { get; set; }

		public string SALES_OFF { get; set; }

		public string REQ_DATE_H { get; set; }

		public string NAME { get; set; }
		
		public string PURCH_NO_C { get; set; }
		public string CURRENCY { get; set; }
		[XmlAttribute("SEGMENT")]
		public string SEGMENT { get; set; }
	}


	[XmlType("E1BPSDITM")]
	public class E1BPSDITM
	{
		public string ITM_NUMBER { get; set; }
		public string MATERIAL { get; set; }
		public string PLANT { get; set; }
		[XmlAttribute("SEGMENT")] 
		public string SEGMENT { get; set; }
		public string SHORT_TEXT { get; set; }
		
		public E1BPSDITM1 E1BPSDITM1 { get; set; }
	}

	[XmlType("E1BPSDITM1")]
	public class E1BPSDITM1
	{
		[XmlAttribute("SEGMENT")]
		public string SEGMENT { get; set; }
	}

	[XmlType("E1BPSDITMX")]
	public class E1BPSDITMX
	{
		public string ITM_NUMBER { get; set; }
		public string UPDATEFLAG { get; set; }
		public string MATERIAL { get; set; }
		public string PLANT { get; set; }
		[XmlAttribute("SEGMENT")]
		public string SEGMENT { get; set; }
		public string SHORT_TEXT { get; set; }
	}

	[XmlType("E1BPPARNR")]
	public class E1BPPARNR
	{
		public string PARTN_ROLE { get; set; }
		public string PARTN_NUMB { get; set; }
		[XmlAttribute("SEGMENT")]
		public string SEGMENT { get; set; }
	}
	[XmlType("E1BPSCHDL")]
	public class E1BPSCHDL
	{
		public string ITM_NUMBER { get; set; }
		public string REQ_DATE { get; set; }
		public string REQ_QTY { get; set; }
		[XmlAttribute("SEGMENT")]
		public string SEGMENT { get; set; }
	}
	
	[XmlRoot("E1BPSCHDLX")]
	public class E1BPSCHDLX
	{
		[XmlElement(ElementName = "ITM_NUMBER")]
		public string ITM_NUMBER { get; set; }

		
		[XmlElement(ElementName = "UPDATEFLAG")]
		public string UPDATEFLAG { get; set; }
		
		[XmlElement(ElementName = "REQ_DATE")]
		public string REQ_DATE { get; set; }
		
		[XmlElement(ElementName = "REQ_QTY")]
		public string REQ_QTY { get; set; }
		[XmlAttribute("SEGMENT")]
		public string SEGMENT;
	}

	[XmlType("E1BPSDTEXT")]
	public class E1BPSDTEXT
	{
		[XmlElement(ElementName = "TEXT_ID")]
		public string TEXT_ID { get; set; }
		[XmlElement(ElementName = "LANGU")]
		public string LANGU { get; set; }
		[XmlElement(ElementName = "TEXT_LINE")]
		public string TEXT_LINE { get; set; }
		[XmlAttribute("SEGMENT")]
		public string SEGMENT;
	}

	[XmlType("E1INQUIRY_CREATEFROMDATA2")]
	public class E1INQUIRY_CREATEFROMDATA2
	{
	    [XmlElement(ElementName = "E1BPSDHD1")]
		public E1BPSDHD1 E1BPSDHD1 { get; set; }
		[XmlElement(ElementName = "E1BPSDHD1X")]
		public E1BPSDHD1X E1BPSDHD1X { get; set; }
		[XmlElement("E1BPSDITM")]
		public List<E1BPSDITM> E1BPSDITM { get; set; }
		[XmlElement("E1BPSDITMX")]
		public List<E1BPSDITMX> E1BPSDITMX { get; set; }
		[XmlElement(ElementName = "E1BPPARNR")]
		public E1BPPARNR E1BPPARNR { get; set; }
		[XmlElement("E1BPSCHDL")]
		public List<E1BPSCHDL> E1BPSCHDL { get; set; }
		[XmlElement("E1BPSCHDLX")]
		public List<E1BPSCHDLX> E1BPSCHDLX { get; set; }
		
		[XmlElement(ElementName = "E1BPSDTEXT")]
		public E1BPSDTEXT E1BPSDTEXT { get; set; }
		[XmlAttribute("SEGMENT")]
		public string SEGMENT;
	}
	
	[XmlType("IDOC")]
	public class IDOC
	{
		[XmlElement(ElementName = "EDI_DC40")]
		public EDI_DC40 EDI_DC40 { get; set; }
		[XmlElement(ElementName = "E1INQUIRY_CREATEFROMDATA2")]
		public E1INQUIRY_CREATEFROMDATA2 E1INQUIRY_CREATEFROMDATA2 { get; set; }

		[XmlAttribute("BEGIN")]
		public string BEGIN { get; set; }
	}

	[XmlRoot("INQUIRY_CREATEFROMDATA201", IsNullable = false)]
	public class INQUIRY_CREATEFROMDATA201
	{
		[XmlElement(ElementName = "IDOC")]
		public IDOC IDOC { get; set; }
	}
}
 