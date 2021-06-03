<?xml version="1.0" encoding="UTF-8" ?>

<xsl:stylesheet version="1.0"
        xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
        xmlns:msxsl="urn:schemas-microsoft-com:xslt"
        xmlns:DominoFormat="urn:domino-function">



  <xsl:output method="html" />

<xsl:decimal-format name="German"
  decimal-separator=","
  grouping-separator="." />
  
  
<xsl:decimal-format name="US"
  decimal-separator="."
  grouping-separator="," />

<xsl:template match="*">
   <xsl:apply-templates />
</xsl:template>

<xsl:template match="/">

<xsl:variable name="SortBY">
	  <xsl:value-of select="/Tran/Res/res"/>
</xsl:variable>

<html>
<head>
<title>DPF RFC Service Report</title>
<xsl:text disable-output-escaping="yes"><![CDATA[
<style type="text/css">
body {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 9px;
	color: #666666;
	background-color: #FFFFFF;
	margin: 0px;
	padding: 0px;
}
form {
	margin: 0px;
	padding: 0px;
}


TD, UL, OL, LI {
   font-family: Verdana, Arial, Helvetica;
   font-weight: normal;
   font-size: 9px;
   color: #004181;
   text-decoration: none;
}
TD.contenttitle
{
	FONT-FAMILY:verdana;
	font-size: 14pt;	
	color: #0e3d6b;
}
TD.contentitemb
{
	FONT-FAMILY:verdana;
	font-size: 8pt;	
	FONT-WEIGHT: bold
}

@media screen,print 
{
.tblHead {
   background-color: #B7BAD9;
   padding-left:2;
   padding-top:2;
   padding-bottom:2;
   margin-top:2;
}
.fcol {
   border: solid 1px #FFFFFF;
}
.col {
   border: solid 1px #FFFFFF;
   border-left-width:0;
}
.tblRow {
   padding-left:2;
   padding-top:2;
   padding-bottom:2;
   margin-top:2;
   background-color: #EF081E;
}
.fcolitem
{
   background-color: #E7E9F5;
   border: solid 1px #FFFFFF;
   border-top:0;
   font-size: 10px;
   font-family: Helvetica, Verdana, Arial;
}
.colitem
{
   background-color: #E7E9F5;
   border: solid 1px #FFFFFF;
   border-left-width:0;
   border-top:0;
   font-family: Helvetica,Verdana, Arial;
   font-size: 10px;
}
.colitem a
{
   color: #0000FF;
}
.colitemredalert
{
   background-color: #E19379;
   border: solid 1px #FFFFFF;
   border-left-width:0;
   border-top:0;
   font-family: Helvetica,Verdana, Arial;
   font-size: 10px;
}
.colitemredalert a
{
   color: #0000FF;
}
.colitemcustomer
{
   background-color: #F0F000;
   border: solid 1px #FFFFFF;
   border-left-width:0;
   border-top:0;
   font-family: Helvetica,Verdana, Arial;
   font-size: 10px;
}
.colitemcustomer a
{
   color: #0000FF;
}
 }
</style>
]]></xsl:text>
</head>
<body>
<table cellpadding="0" cellspacing="0" width="800" border="0">
<tr>
	<td valign="top">
	
	<!-- Start main table -->
	<table cellpadding="0" cellspacing="0" width="100%" border="0">
	<tr><td width="1" height="20"><DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV></td></tr>
	</table>

	<table cellpadding="0" cellspacing="0" width="100%" border="0">
	<tr>
		<td align="center" class="contenttitle">DPF RFC Service Report / TransactionID: <xsl:value-of select="/Tran/Settings/tranid"/></td>
	</tr>
	<tr><td width="1" height="10"><DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV></td></tr>
	</table>
	
	
	<table cellpadding="0" cellspacing="0" width="100%" border="0">
	<tr><td width="1" height="20"><DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV></td></tr>
	<tr><td class="contenttitle">Quelle</td></tr>
	<tr><td width="1" height="5"><DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV></td></tr>
	<tr><td>------------------------------------------------------------------------------------------</td></tr>
	<tr><td width="1" height="5"><DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV></td></tr>
	</table>

    <table cellpadding="0" cellspacing="0" width="540" border="0">
      <tr>
        <td width="10" rowspan="13">
          <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
        </td>
        <td width="250" class="contentitemb">Hostname</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td width="270" class="contentitemb">
          [<xsl:value-of select="/Tran/Settings/sapip"/>]
        </td>
      </tr>
      <tr>
        <td class="contentitemb">Systemnummer</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td  class="contentitemb">
          [<xsl:value-of select="/Tran/Settings/sapgw"/>]
        </td>
      </tr>
      <tr>
        <td class="contentitemb">Mandant</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td  class="contentitemb">
          [<xsl:value-of select="/Tran/Settings/sapmandant"/>]
        </td>
      </tr>
      <tr>
        <td class="contentitemb">Destination</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td  class="contentitemb">
          [<xsl:value-of select="/Tran/Settings/saprfcdest"/>]
        </td>
      </tr>
      
      
      
      
      <tr>
        <td  class="contentitemb">Buchungskreis</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td  class="contentitemb">
          [<xsl:choose>
            <xsl:when test="(/Tran/Settings/sapbk = '')">übergreifend</xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="/Tran/Settings/sapbk"/>
            </xsl:otherwise>
          </xsl:choose>]
        </td>
      </tr>
      <tr>
        <td  class="contentitemb">Verkaufsorganisation</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td  class="contentitemb">
          [<xsl:choose>
            <xsl:when test="(/Tran/Settings/sapvo = '')">übergreifend</xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="/Tran/Settings/sapvo"/>
            </xsl:otherwise>
          </xsl:choose>]
        </td>
      </tr>
      <tr>
        <td  class="contentitemb">Werk</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td  class="contentitemb">
          [<xsl:choose>
            <xsl:when test="(/Tran/Settings/sapwk = '')">übergreifend</xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="/Tran/Settings/sapwk"/>
            </xsl:otherwise>
          </xsl:choose>]
        </td>
      </tr>
      
      
      
      <tr>
        <td  class="contentitemb">Report aus</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td  class="contentitemb">
          [<xsl:value-of select="/Tran/Settings/saptrn"/>]
        </td>
      </tr>
      <tr>
        <td  class="contentitemb">Benutzer</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td  class="contentitemb">
          [<xsl:value-of select="/Tran/Settings/sapusr"/>]
        </td>
      </tr>
      <tr>
        <td  class="contentitemb">Adressen geprüft</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td  class="contentitemb">
          [<xsl:value-of select="/Tran/Settings/totalrecords"/>]
        </td>
      </tr>
      <tr>
        <td  class="contentitemb">Adressen mit Treffer</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td  class="contentitemb">
          [<xsl:value-of select="/Tran/Settings/matchedrecord"/>]
        </td>
      </tr>
      <tr>
        <td  class="contentitemb">Höchster Treffer in %</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td  class="contentitemb">
          [<xsl:value-of select="/Tran/Settings/maxmatchprcnt"/>]
        </td>
      </tr>
      <tr>
        <td class="contentitemb">TransactionID</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td  class="contentitemb">
          [<xsl:value-of select="/Tran/Settings/tranid"/>]
        </td>
      </tr>
    </table>



    <table cellpadding="0" cellspacing="0" width="100%" border="0">
      <tr>
        <td width="1" height="20">
          <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
        </td>
      </tr>
      <tr>
        <td class="contenttitle">Laufzeit</td>
      </tr>
      <tr>
        <td width="1" height="5">
          <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
        </td>
      </tr>
      <tr>
        <td>------------------------------------------------------------------------------------------</td>
      </tr>
      <tr>
        <td width="1" height="5">
          <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
        </td>
      </tr>
    </table>


    <table cellpadding="0" cellspacing="0" width="540" border="0">
      <tr>
        <td width="10" rowspan="2">
          <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
        </td>
        <td width="250" class="contentitemb">gestartet</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td width="270" class="contentitemb">
          [<xsl:value-of select="DominoFormat:GetDateTime(/Tran/Settings/startedon,'G')"/>]
        </td>
      </tr>
      <tr>
        <td class="contentitemb">beendet</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td  class="contentitemb">
          [<xsl:value-of select="DominoFormat:GetDateTime(/Tran/Settings/finishedon,'G')"/>]
        </td>
      </tr>
    </table>
    
    
    
	<table cellpadding="0" cellspacing="0" width="100%" border="0">
	<tr><td width="1" height="20"><DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV></td></tr>
	<tr><td class="contenttitle">Matching Einstellungen</td></tr>
	<tr><td width="1" height="5"><DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV></td></tr>
	<tr><td>------------------------------------------------------------------------------------------</td></tr>
	<tr><td width="1" height="5"><DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV></td></tr>
	</table>


    <table cellpadding="0" cellspacing="0" width="540" border="0">
      <tr>
        <td width="10" rowspan="7">
          <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
        </td>
        <td width="250" class="contentitemb">Variante</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td width="270" class="contentitemb">
          [<xsl:value-of select="/Tran/Settings/sapvar"/>]
        </td>
      </tr>
      <tr>
        <td class="contentitemb">Boykottlisten Content</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td  class="contentitemb">[FULL]</td>
      </tr>
      <tr>
        <td class="contentitemb">von Version</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td  class="contentitemb">
          [<xsl:value-of select="/Tran/Settings/verid"/>]
        </td>
      </tr>
      <tr>
        <td class="contentitemb">betroffene Boykottlisten</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td  class="contentitemb">
          [<xsl:value-of select="/Tran/Settings/denlists"/>]
        </td>
      </tr>


      <tr>
        <td class="contentitemb">geprüfte Felder</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td  class="contentitemb">
          <xsl:choose>
          <xsl:when test="(1 = /Tran/Settings/checktypeid)">
            [nur Namen]
          </xsl:when>
          <xsl:when test="(2 = /Tran/Settings/checktypeid)">
            [nur Adresse]
          </xsl:when>
          <xsl:when test="(3 = /Tran/Settings/checktypeid)">
            [Name und Adresse
          </xsl:when>
          <xsl:when test="(4 = /Tran/Settings/checktypeid)">
            [alle Felder]
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="/Tran/Settings/checktype"/>
          </xsl:otherwise>
        </xsl:choose>
        </td>
      </tr>
      <tr>
        <td  class="contentitemb">verwendeter Algorithmus</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td  class="contentitemb">
          <xsl:choose>
          <xsl:when test="(1 = /Tran/Settings/matchtypeid)">
            [Stöbern]
          </xsl:when>
          <xsl:when test="(2 = /Tran/Settings/matchtypeid)">
            [Ähnlichkeiten]
          </xsl:when>
          <xsl:when test="(3 = /Tran/Settings/matchtypeid)">
            [echte Treffer]
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="/Tran/Settings/matchtype"/>
          </xsl:otherwise>
        </xsl:choose>
        </td>
      </tr>
      <tr>
        <td  class="contentitemb">Red Alert</td>
        <td width="10" class="contentitemb" align="center">:</td>
        <td  class="contentitemb">
          [<xsl:value-of select="/Tran/Settings/redalert"/>]
        </td>
      </tr>

    </table>


    <table cellpadding="0" cellspacing="0" width="100%" border="0">
	<tr><td width="1" height="20"><DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV></td></tr>
	</table>
	
	<table cellpadding="0" cellspacing="0" width="800" border="0">
	<tr class="tblHead">
		<td width="30" class="fcol" align="center">%</td>
		<td width="30" class="col">CLID</td>
		<td width="30" class="col">SRCID</td>
		<td width="150" class="col">Name</td>
		<td width="150" class="col">Strasse</td>
		<td width="90" class="col">Ort</td>
		<td width="50" class="col">PLZ</td>
		<td width="30" class="col">Staat</td>
		<td width="30" class="col">Land</td>
		<td width="50" class="col">Listen</td>
		<td width="70" class="col">Datum</td>
		<td width="90" class="col">Details</td>
	</tr>
	<tr><td colspan="12" height="5"><DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV></td></tr>
	<tr><TD vAlign="top" bgColor="#4F8EB6" colspan="12" height="1"><DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV></TD></tr>
	<tr><td colspan="12" height="5"><DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV></td></tr>
		
	<xsl:apply-templates select="/Tran/Src"/>	
	</table>
	<!-- End main table -->
	
	</td>
</tr>
</table>

</body>
</html>

</xsl:template> 

<xsl:template match="/Tran/Src">

	<tr>
		<TD class="colitemcustomer" align="center">-</TD>
		<TD class="colitemcustomer" align="center"><xsl:value-of select="DominoFormat:ReplaceEmpty(clid)"/></TD>
		<TD class="colitemcustomer" align="center"><xsl:value-of select="DominoFormat:ReplaceEmpty(sourceid)"/></TD>
		<TD class="colitemcustomer"><xsl:value-of select="DominoFormat:ReplaceEmpty(name)"/></TD>
		<TD class="colitemcustomer"><xsl:value-of select="DominoFormat:ReplaceEmpty(street)"/></TD>
		<TD class="colitemcustomer"><xsl:value-of select="DominoFormat:ReplaceEmpty(city)"/></TD>
		<TD class="colitemcustomer"><xsl:value-of select="DominoFormat:ReplaceEmpty(zip)"/></TD>
		<TD class="colitemcustomer">-</TD>
		<TD class="colitemcustomer"><xsl:value-of select="DominoFormat:ReplaceEmpty(country)"/></TD>	
		<TD class="colitemcustomer">-</TD>	
		<TD class="colitemcustomer">-</TD>	
		<TD class="colitemcustomer">-</TD>	
	</tr>
	<tr><td colspan="11" height="5"><DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV></td></tr>

	<xsl:for-each select="/Tran/Res">
	<xsl:sort data-type="number" order="descending" select="res" />
	
		<tr>
			<TD align="center">
				<xsl:choose>
					<xsl:when test="(res > /Tran/Settings/redalert)">
						<xsl:attribute name="class">colitemredalert</xsl:attribute>
						<xsl:value-of select="DominoFormat:ReplaceEmpty(res)"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="class">colitem</xsl:attribute>
						<xsl:value-of select="DominoFormat:ReplaceEmpty(res)"/>
					</xsl:otherwise>
				</xsl:choose>
			</TD>
			<TD >
				<xsl:choose>
					<xsl:when test="(res > /Tran/Settings/redalert)">
						<xsl:attribute name="class">colitemredalert</xsl:attribute>
						-
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="class">colitem</xsl:attribute>
						-
					</xsl:otherwise>
				</xsl:choose>
			</TD>
			<TD >
				<xsl:choose>
					<xsl:when test="(res > /Tran/Settings/redalert)">
						<xsl:attribute name="class">colitemredalert</xsl:attribute>
						-
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="class">colitem</xsl:attribute>
						-
					</xsl:otherwise>
				</xsl:choose>
			</TD>
			<TD >
				<xsl:choose>
					<xsl:when test="(res > /Tran/Settings/redalert)">
						<xsl:attribute name="class">colitemredalert</xsl:attribute>
						<xsl:value-of select="DominoFormat:ReplaceEmpty(name)"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="class">colitem</xsl:attribute>
						<xsl:value-of select="DominoFormat:ReplaceEmpty(name)"/>
					</xsl:otherwise>
				</xsl:choose>
			</TD>
			<TD >
				<xsl:choose>
					<xsl:when test="(res > /Tran/Settings/redalert)">
						<xsl:attribute name="class">colitemredalert</xsl:attribute>
						<xsl:value-of select="DominoFormat:ReplaceEmpty(street)"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="class">colitem</xsl:attribute>
						<xsl:value-of select="DominoFormat:ReplaceEmpty(street)"/>
					</xsl:otherwise>
				</xsl:choose>
			</TD>	
			<TD >
				<xsl:choose>
					<xsl:when test="(res > /Tran/Settings/redalert)">
						<xsl:attribute name="class">colitemredalert</xsl:attribute>
						<xsl:value-of select="DominoFormat:ReplaceEmpty(city)"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="class">colitem</xsl:attribute>
						<xsl:value-of select="DominoFormat:ReplaceEmpty(city)"/>
					</xsl:otherwise>
				</xsl:choose>
			</TD>
			<TD >
				<xsl:choose>
					<xsl:when test="(res > /Tran/Settings/redalert)">
						<xsl:attribute name="class">colitemredalert</xsl:attribute>
						<xsl:value-of select="DominoFormat:ReplaceEmpty(zip)"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="class">colitem</xsl:attribute>
						<xsl:value-of select="DominoFormat:ReplaceEmpty(zip)"/>
					</xsl:otherwise>
				</xsl:choose>
			</TD>
			<TD >
				<xsl:choose>
					<xsl:when test="(res > /Tran/Settings/redalert)">
						<xsl:attribute name="class">colitemredalert</xsl:attribute>
						<xsl:value-of select="DominoFormat:ReplaceEmpty(state)"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="class">colitem</xsl:attribute>
						<xsl:value-of select="DominoFormat:ReplaceEmpty(state)"/>
					</xsl:otherwise>
				</xsl:choose>
			</TD>
			<TD >
				<xsl:choose>
					<xsl:when test="(res > /Tran/Settings/redalert)">
						<xsl:attribute name="class">colitemredalert</xsl:attribute>
						<xsl:value-of select="DominoFormat:ReplaceEmpty(country)"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="class">colitem</xsl:attribute>
						<xsl:value-of select="DominoFormat:ReplaceEmpty(country)"/>
					</xsl:otherwise>
				</xsl:choose>
			</TD>	
			<TD >


        <xsl:choose>
          <xsl:when test="(res > /Tran/Settings/redalert)">
            <xsl:attribute name="class">colitemredalert</xsl:attribute>
          </xsl:when>
          <xsl:otherwise>
            <xsl:attribute name="class">colitem</xsl:attribute>
          </xsl:otherwise>
        </xsl:choose>


        <xsl:choose>
          <xsl:when test="(denid &lt; 10000000)">

            <a>
              <xsl:attribute name="href">
                http://contentinfo.sapper.de/?id=<xsl:value-of select="denid"/>
              </xsl:attribute>
              <xsl:attribute name="target">_new</xsl:attribute>
              <xsl:value-of select="DominoFormat:ReplaceEmpty(dentype)"/>
            </a>
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="DominoFormat:ReplaceEmpty(dentype)"/>
          </xsl:otherwise>
        </xsl:choose>
        
        
			</TD>	
			<TD >
				<xsl:choose>
					<xsl:when test="(res > /Tran/Settings/redalert)">
						<xsl:attribute name="class">colitemredalert</xsl:attribute>
						<xsl:value-of select="DominoFormat:GetDateTime(date,'d')"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="class">colitem</xsl:attribute>
						<xsl:value-of select="DominoFormat:GetDateTime(date,'d')"/>
					</xsl:otherwise>
				</xsl:choose>
			</TD>	
			<TD >
        
        <xsl:choose>
          <xsl:when test="(res > /Tran/Settings/redalert)">
            <xsl:attribute name="class">colitemredalert</xsl:attribute>
          </xsl:when>
          <xsl:otherwise>
            <xsl:attribute name="class">colitem</xsl:attribute>
          </xsl:otherwise>
        </xsl:choose>
        <xsl:value-of select="DominoFormat:ReplaceEmpty(notes)"/>
			
			</TD>			
		</tr>
		
	</xsl:for-each>

	<tr><TD vAlign="top" bgColor="#4F8EB6" colspan="12" height="3"><DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV></TD></tr>
	<tr><td colspan="12" height="15"><DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV></td></tr>


</xsl:template>



</xsl:stylesheet>