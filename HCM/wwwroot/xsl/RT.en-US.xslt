<?xml version="1.0" encoding="UTF-8" ?>

<xsl:stylesheet version="1.0"
        xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:DominoFormat="urn:domino-function">
        
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


<xsl:text disable-output-escaping="yes"><![CDATA[
<style type="text/css">
body {
	color: #666666;
}

TD, UL, OL, LI {

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

<table class="table table-striped">
<tr>
	<td valign="top">
	
	<table cellpadding="0" cellspacing="0" width="100%" border="0">
	<tr>
		<td align="center" class="contenttitle">DPF Real Time Service Report / TransactionID: <xsl:value-of select="/Tran/Settings/tranid"/></td>
	</tr>
	<tr><td width="1" height="10"><DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV></td></tr>
	<tr>
		<td align="center" class="contenttitle">ProfileID : <xsl:value-of select="/Tran/Settings/profileid"/></td>
	</tr>
	</table>
	
	
	<table cellpadding="0" cellspacing="0" width="100%" border="0">
	<tr><td class="contenttitle">Summary</td></tr>
	<tr><td>----------------------------------------------------------------</td></tr>
	</table>	
		
	<table cellpadding="0" cellspacing="0" width="440" border="0">
	<tr>
		<td width="10" rowspan="6"><DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV></td>
		<td width="150" class="contentitemb">Profile Name</td>
		<td width="10" class="contentitemb" align="center">:</td>
		<td width="270" class="contentitemb"><xsl:value-of select="/Tran/Settings/profilename"/></td>
	</tr>
	<tr>
		<td class="contentitemb">ProfileID</td>
		<td width="10" class="contentitemb" align="center">:</td>
		<td  class="contentitemb"><xsl:value-of select="/Tran/Settings/profileid"/></td>
	</tr>
	<tr>
		<td class="contentitemb">TransactionID</td>
		<td width="10" class="contentitemb" align="center">:</td>
		<td  class="contentitemb"><xsl:value-of select="/Tran/Settings/tranid"/></td>
	</tr>
	<tr>
		<td class="contentitemb">Started</td>
		<td width="10" class="contentitemb" align="center">:</td>
		<td  class="contentitemb"><xsl:value-of select="DominoFormat:GetDateTime(/Tran/Settings/startedon,'g')"/></td>
	</tr>
	<tr>
		<td  class="contentitemb">Total Matches</td>
		<td width="10" class="contentitemb" align="center">:</td>
		<td  class="contentitemb"><xsl:value-of select="/Tran/Settings/totalmatches"/></td>
	</tr>
	<tr>
		<td  class="contentitemb">Max percent</td>
		<td width="10" class="contentitemb" align="center">:</td>
		<td  class="contentitemb"><xsl:value-of select="/Tran/Settings/maxmatchprcnt"/></td>
	</tr>		
	</table>	
	
	
	<table cellpadding="0" cellspacing="0" width="100%" border="0">
	<tr><td class="contenttitle">Matching Settings</td></tr>
	<tr><td>----------------------------------------------------------------</td></tr>
	</table>	
	
		
	<table cellpadding="0" cellspacing="0" width="440" border="0">
	<tr>
		<td width="10" rowspan="5"><DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV></td>
		<td width="150" class="contentitemb">Boycott Lists</td>
		<td width="10" class="contentitemb" align="center">:</td>
		<td width="270" class="contentitemb"><xsl:value-of select="/Tran/Settings/denlists"/></td>
	</tr>
	<tr>
		<td class="contentitemb">Boycott Lists Version</td>
		<td width="10" class="contentitemb" align="center">:</td>
		<td  class="contentitemb"><xsl:value-of select="/Tran/Settings/verid"/></td>
	</tr>
	<tr>
		<td class="contentitemb">Check Type</td>
		<td width="10" class="contentitemb" align="center">:</td>
		<td  class="contentitemb"><xsl:choose>
					<xsl:when test="(1 = /Tran/Settings/checktype)">
						name only
					</xsl:when>
					<xsl:when test="(2 = /Tran/Settings/checktype)">
						address only
					</xsl:when>
					<xsl:when test="(3 = /Tran/Settings/checktype)">
						name and address
					</xsl:when>
					<xsl:when test="(4 = /Tran/Settings/checktype)">
						all fields
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="/Tran/Settings/checktype"/>
					</xsl:otherwise>
				</xsl:choose></td>
	</tr>
	<tr>
		<td  class="contentitemb">Match Type</td>
		<td width="10" class="contentitemb" align="center">:</td>
		<td  class="contentitemb"><xsl:choose>
					<xsl:when test="(1 = /Tran/Settings/matchtype)">
						scout about
					</xsl:when>
					<xsl:when test="(2 = /Tran/Settings/matchtype)">
						similar
					</xsl:when>
					<xsl:when test="(3 = /Tran/Settings/matchtype)">
						accurate
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="/Tran/Settings/matchtype"/>
					</xsl:otherwise>
				</xsl:choose></td>
	</tr>
	<tr>
		<td  class="contentitemb">Red Alert</td>
		<td width="10" class="contentitemb" align="center">:</td>
		<td  class="contentitemb"><xsl:value-of select="/Tran/Settings/redalert"/></td>
	</tr>
		
	</table>			
		
	
	
	<table class="table table-striped col-sm-12">
    <thead>
			<tr>
				<th scope="col" align="center">%</th>
				<th scope="col">CLID</th>
				<th scope="col">SRCID</th>
				<th scope="col">Name</th>
				<th scope="col">Street</th>
				<th scope="col">City</th>
				<th scope="col">Zip</th>
				<th scope="col">State</th>
				<th scope="col">Country</th>
				<th scope="col">List</th>
				<th scope="col">Date</th>
				<th scope="col">Details</th>
			</tr>
  </thead>

		
	
  <xsl:apply-templates select="/Tran/Src"/>	
	</table>
	<!-- End main table -->
	
	</td>
</tr>
</table>



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




</xsl:template>



</xsl:stylesheet>
