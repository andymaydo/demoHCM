<?xml version="1.0" encoding="UTF-8" ?>

<xsl:stylesheet version="1.0"
        xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:DominoFormat="urn:domino-function">

  <xsl:output method="html" />

  <xsl:decimal-format name="German"
    decimal-separator=","
    grouping-separator="." />

  <xsl:template match="*">
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="/">
    <html>
      <head>
        <title>SAPPER HCM</title>
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
a {
	color: #66cc33;
	text-decoration: none;
}

TD, UL, OL, LI {
   font-family: Verdana, Arial, Helvetica;
   font-weight: normal;
   font-size: 11px;
   color: #004181;
   text-decoration: none;
}
TD.contenttitle
{
	FONT-FAMILY:verdana;
	font-size: 14px;	
	color: #0e3d6b;
font-weight: bold;
}
TD.contentitemb
{
	FONT-FAMILY:verdana;
	font-size: 11px;	
	FONT-WEIGHT: bold;
  padding-left:10px;
}

@media screen,print 
{
.tblHead {
   background-color: #dddddd;
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
                <tr>
                  <td width="1" height="20">
                    <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
                  </td>
                </tr>
              </table>

              <table cellpadding="0" cellspacing="0" width="100%" border="0">
                <tr>
                  <td align="center" class="contenttitle">DPF Match Report </td>
                </tr>
                <tr>
                  <td width="1" height="10">
                    <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
                  </td>
                </tr>

                <xsl:if test="/Root/Batch/Settings/profileid or /Root/Tran/Settings/profileid">
                <tr>
                  <td align="center" class="contenttitle">
                    ProfileID : <xsl:value-of select="/Root/Batch/Settings/profileid"/><xsl:value-of select="/Root/Tran/Settings/profileid"/>
                  </td>
                </tr>
                </xsl:if>  
                  
              </table>

              <table cellpadding="0" cellspacing="0" width="100%" border="0">
                <tr>
                  <td width="1" height="20">
                    <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
                  </td>
                </tr>
                <tr>
                  <td class="contenttitle">Summary</td>
                </tr>
                <tr>
                  <td width="1" height="5">
                    <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
                  </td>
                </tr>
                <tr>
                  <td>----------------------------------------------------------------</td>
                </tr>
                <tr>
                  <td width="1" height="5">
                    <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
                  </td>
                </tr>
              </table>

              <table cellpadding="0" cellspacing="0" width="440" border="0">

                <xsl:if test="/Root/Batch/Settings/profilename or /Root/Tran/Settings/profilename">
                <tr>
                  <td width="150" class="contentitemb">Profile Name</td>
                  <td width="10" class="contentitemb" align="center">:</td>
                  <td width="270" class="contentitemb">
                    <xsl:value-of select="/Root/Batch/Settings/profilename"/><xsl:value-of select="/Root/Tran/Settings/profilename"/>
                  </td>
                </tr>
                </xsl:if>

                <xsl:if test="/Root/Tran/Settings/sapip">
                <tr>
                  <td width="150" class="contentitemb">SAPIP</td>
                  <td width="10" class="contentitemb" align="center">:</td>
                  <td width="270" class="contentitemb">
                    <xsl:value-of select="/Root/Tran/Settings/sapip"/>
                  </td>
                </tr>
                <tr>
                  <td class="contentitemb">SAPGW</td>
                  <td width="10" class="contentitemb" align="center">:</td>
                  <td  class="contentitemb">
                    <xsl:value-of select="/Root/Tran/Settings/sapgw"/>
                  </td>
                </tr>
                <tr>
                  <td class="contentitemb">Sap Destination</td>
                  <td width="10" class="contentitemb" align="center">:</td>
                  <td  class="contentitemb">
                    <xsl:value-of select="/Root/Tran/Settings/saprfcdest"/>
                  </td>
                </tr>
                <tr>
                  <td class="contentitemb">Mandant</td>
                  <td width="10" class="contentitemb" align="center">:</td>
                  <td  class="contentitemb">
                    <xsl:value-of select="/Root/Tran/Settings/sapmandant"/>
                  </td>
                </tr>
                </xsl:if>

                <xsl:if test="/Root/Batch/Settings/profileid or /Root/Tran/Settings/profileid">
                <tr>
                  <td class="contentitemb">ProfileID</td>
                  <td width="10" class="contentitemb" align="center">:</td>
                  <td  class="contentitemb">
                    <xsl:value-of select="/Root/Batch/Settings/profileid"/>
                    <xsl:value-of select="/Root/Tran/Settings/profileid"/>
                  </td>
                </tr>
                </xsl:if>

                <xsl:if test="/Root/Batch/Settings/batchid">
                <tr>
                  <td class="contentitemb">Transaction ID</td>
                  <td width="10" class="contentitemb" align="center">:</td>
                  <td  class="contentitemb">
                    <xsl:value-of select="/Root/Batch/Settings/batchid"/>
                  </td>
                </tr>
                </xsl:if>

                <xsl:if test="/Root/Tran/Settings/tranid">
                  <tr>
                    <td class="contentitemb">Transaction ID</td>
                    <td width="10" class="contentitemb" align="center">:</td>
                    <td  class="contentitemb">
                      <xsl:value-of select="/Root/Tran/Settings/tranid"/>
                    </td>
                  </tr>
                </xsl:if>
                
                <tr>
                  <td class="contentitemb">Started</td>
                  <td width="10" class="contentitemb" align="center">:</td>
                  <td  class="contentitemb">
                    <xsl:value-of select="DominoFormat:GetDateTime(/Root/Batch/Settings/startedon,'g')"/>
                    <xsl:value-of select="DominoFormat:GetDateTime(/Root/Tran/Settings/startedon,'g')"/>
                  </td>
                </tr>

                <xsl:if test="((/Root/Batch/Settings/matchedrecord != '') or (/Root/Tran/Settings/matchedrecord != '') )">
                <tr>
                  <td class="contentitemb">Finished</td>
                  <td width="10" class="contentitemb" align="center">:</td>
                  <td  class="contentitemb">
                    <xsl:value-of select="DominoFormat:GetDateTime(/Root/Batch/Settings/finishedon,'g')"/>
                    <xsl:value-of select="DominoFormat:GetDateTime(/Root/Tran/Settings/finishedon,'g')"/>
                  </td>
                </tr>
                </xsl:if>
                
                <tr>
                  <td  class="contentitemb">Total records</td>
                  <td width="10" class="contentitemb" align="center">:</td>
                  <td  class="contentitemb">
                    <xsl:value-of select="/Root/Batch/Settings/totalrecords"/>
                    <xsl:value-of select="/Root/Tran/Settings/totalrecords"/>
                  </td>
                </tr>

                <xsl:if test="((/Root/Batch/Settings/matchedrecord != '') or (/Root/Tran/Settings/matchedrecord != '') )">
                <tr>
                  <td  class="contentitemb">Matched records</td>
                  <td width="10" class="contentitemb" align="center">:</td>
                  <td  class="contentitemb">
                    <xsl:value-of select="/Root/Batch/Settings/matchedrecord"/>
                    <xsl:value-of select="/Root/Tran/Settings/matchedrecord"/>
                  </td>
                </tr>
                </xsl:if>

                <xsl:if test="((/Root/Batch/Settings/totalmatches != '') or (/Root/Tran/Settings/totalmatches != '') )">
                <tr>
                  <td  class="contentitemb">Total matches</td>
                  <td width="10" class="contentitemb" align="center">:</td>
                  <td  class="contentitemb">
                    <xsl:value-of select="/Root/Batch/Settings/totalmatches"/>
                    <xsl:value-of select="/Root/Tran/Settings/totalmatches"/>
                  </td>
                </tr>
                </xsl:if>

                <xsl:if test="((/Root/Batch/Settings/maxmatchprcnt != '') or (/Root/Tran/Settings/maxmatchprcnt != '') )">
                <tr>
                  <td  class="contentitemb">Max value</td>
                  <td width="10" class="contentitemb" align="center">:</td>
                  <td  class="contentitemb">
                    <xsl:value-of select="/Root/Batch/Settings/maxmatchprcnt"/>
                    <xsl:value-of select="/Root/Tran/Settings/maxmatchprcnt"/>
                  </td>
                </tr>
                </xsl:if>
              </table>


              <table cellpadding="0" cellspacing="0" width="100%" border="0">
                <tr>
                  <td width="1" height="20">
                    <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
                  </td>
                </tr>
                <tr>
                  <td class="contenttitle">Matching Results</td>
                </tr>
                <tr>
                  <td width="1" height="5">
                    <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
                  </td>
                </tr>
                <tr>
                  <td>----------------------------------------------------------------</td>
                </tr>
                <tr>
                  <td width="1" height="5">
                    <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
                  </td>
                </tr>
              </table>


              <table cellpadding="0" cellspacing="0" width="440" border="0">
                <tr>
                  <td width="150" class="contentitemb">Boycott Lists</td>
                  <td width="10" class="contentitemb" align="center">:</td>
                  <td width="270" class="contentitemb">
                    <xsl:value-of select="/Root/Batch/Settings/denlists"/>
                    <xsl:value-of select="/Root/Tran/Settings/denlists"/>
                  </td>
                </tr>
                <tr>
                  <td class="contentitemb">Boycott Lists Version</td>
                  <td width="10" class="contentitemb" align="center">:</td>
                  <td  class="contentitemb">
                    <xsl:value-of select="/Root/Batch/Settings/verid"/>
                    <xsl:value-of select="/Root/Tran/Settings/verid"/>
                  </td>
                </tr>
                <tr>
                  <td class="contentitemb">Check Type</td>
                  <td width="10" class="contentitemb" align="center">:</td>
                  <td  class="contentitemb">
                    <xsl:choose>
                      <xsl:when test="((1 = /Root/Batch/Settings/checktype) or (1 = /Root/Tran/Settings/checktype))">
                        name only
                      </xsl:when>
                      <xsl:when test="((2 = /Root/Batch/Settings/checktype) or (2 = /Root/Tran/Settings/checktype) )">
                        address only
                      </xsl:when>
                      <xsl:when test="((3 = /Root/Batch/Settings/checktype) or (3 = /Root/Tran/Settings/checktype) )">
                        name and address
                      </xsl:when>
                      <xsl:when test="((4 = /Root/Batch/Settings/checktype) or (4 = /Root/Tran/Settings/checktype) )">
                        all fields
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select="/Root/Batch/Settings/checktype"/>
                        <xsl:value-of select="/Root/Tran/Settings/checktype"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </td>
                </tr>
                <tr>
                  <td  class="contentitemb">Match Type</td>
                  <td width="10" class="contentitemb" align="center">:</td>
                  <td  class="contentitemb">
                    <xsl:choose>
                      <xsl:when test="((1 = /Root/Batch/Settings/matchtype) or (1 = /Root/Tran/Settings/matchtype))">
                        scout about
                      </xsl:when>
                      <xsl:when test="((2 = /Root/Batch/Settings/matchtype) or (2 = /Root/Tran/Settings/matchtype))">
                        similar
                      </xsl:when>
                      <xsl:when test="((3 = /Root/Batch/Settings/matchtype) or (3 = /Root/Tran/Settings/matchtype))">
                        accurate
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select="/Root/Batch/Settings/matchtype"/>
                        <xsl:value-of select="/Root/Tran/Settings/matchtype"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </td>
                </tr>
                <tr>
                  <td  class="contentitemb">Red Alert</td>
                  <td width="10" class="contentitemb" align="center">:</td>
                  <td  class="contentitemb">
                    <xsl:value-of select="/Root/Batch/Settings/redalert"/>
                    <xsl:value-of select="/Root/Tran/Settings/redalert"/>
                  </td>
                </tr>

              </table>


              <table cellpadding="0" cellspacing="0" width="100%" border="0">
                <tr>
                  <td width="1" height="20">
                    <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
                  </td>
                </tr>
              </table>

              <table cellpadding="0" cellspacing="0" width="800" border="0">
                <tr class="tblHead">
                  <td width="30" class="fcol" align="center">%</td>
                  <td width="30" class="col">CLID</td>
                  <td width="30" class="col">SRCID</td>
                  <td width="150" class="col">Name</td>
                  <td width="150" class="col">Street</td>
                  <td width="90" class="col">City</td>
                  <td width="50" class="col">Zip</td>
                  <td width="30" class="col">State</td>
                  <td width="30" class="col">Country</td>
                  <td width="50" class="col">List</td>
                  <td width="70" class="col">Date</td>
                  <td width="90" class="col">Details</td>
                </tr>
                <tr>
                  <td colspan="12" height="5">
                    <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
                  </td>
                </tr>
                <tr>
                  <TD vAlign="top" bgColor="#4F8EB6" colspan="12" height="1">
                    <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
                  </TD>
                </tr>
                <tr>
                  <td colspan="12" height="5">
                    <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
                  </td>
                </tr>

                <xsl:for-each select="/Root/Batch/Src">
                  <xsl:sort data-type="number" order="descending" select="res" />

                  <tr>
                    <TD class="colitemcustomer">-</TD>
                    <TD class="colitemcustomer" align="center">
                      <xsl:value-of select="DominoFormat:ReplaceEmpty(clid)"/>
                    </TD>
                    <TD class="colitemcustomer" align="center">
                      <xsl:value-of select="DominoFormat:ReplaceEmpty(sourceid)"/>
                    </TD>
                    <TD class="colitemcustomer">
                      <xsl:value-of select="DominoFormat:ReplaceEmpty(name)"/>
                    </TD>
                    <TD class="colitemcustomer">
                      <xsl:value-of select="DominoFormat:ReplaceEmpty(street)"/>
                    </TD>
                    <TD class="colitemcustomer">
                      <xsl:value-of select="DominoFormat:ReplaceEmpty(city)"/>
                    </TD>
                    <TD class="colitemcustomer">
                      <xsl:value-of select="DominoFormat:ReplaceEmpty(zip)"/>
                    </TD>
                    <TD class="colitemcustomer">-</TD>
                    <TD class="colitemcustomer">
                      <xsl:value-of select="DominoFormat:ReplaceEmpty(country)"/>
                    </TD>
                    <TD class="colitemcustomer">-</TD>
                    <TD class="colitemcustomer">-</TD>
                    <TD class="colitemcustomer">-</TD>
                  </tr>
                  <tr>
                    <td colspan="11" height="5">
                      <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
                    </td>
                  </tr>

                  <xsl:apply-templates select="."/>

                </xsl:for-each>


                <xsl:for-each select="/Root/Tran/Src">
                  <xsl:sort data-type="number" order="descending" select="res" />

                  <tr>
                    <TD class="colitemcustomer">-</TD>
                    <TD class="colitemcustomer" align="center">
                      <xsl:value-of select="DominoFormat:ReplaceEmpty(clid)"/>
                    </TD>
                    <TD class="colitemcustomer" align="center">
                      <xsl:value-of select="DominoFormat:ReplaceEmpty(sourceid)"/>
                    </TD>
                    <TD class="colitemcustomer">
                      <xsl:value-of select="DominoFormat:ReplaceEmpty(name)"/>
                    </TD>
                    <TD class="colitemcustomer">
                      <xsl:value-of select="DominoFormat:ReplaceEmpty(street)"/>
                    </TD>
                    <TD class="colitemcustomer">
                      <xsl:value-of select="DominoFormat:ReplaceEmpty(city)"/>
                    </TD>
                    <TD class="colitemcustomer">
                      <xsl:value-of select="DominoFormat:ReplaceEmpty(zip)"/>
                    </TD>
                    <TD class="colitemcustomer">-</TD>
                    <TD class="colitemcustomer">
                      <xsl:value-of select="DominoFormat:ReplaceEmpty(country)"/>
                    </TD>
                    <TD class="colitemcustomer">-</TD>
                    <TD class="colitemcustomer">-</TD>
                    <TD class="colitemcustomer">-</TD>
                  </tr>
                  <tr>
                    <td colspan="11" height="5">
                      <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
                    </td>
                  </tr>

                  <xsl:apply-templates select="."/>

                </xsl:for-each>

              </table>
              <!-- End main table -->

            </td>
          </tr>
        </table>

      </body>
    </html>

  </xsl:template>

  <xsl:template match="/Root/Batch/Src">
    <xsl:variable name="index" select="index" />

    <xsl:if test="/Root/Batch/Res[index=$index]">



      <xsl:for-each select="/Root/Batch/Res[index=$index]">
        <xsl:sort data-type="number" order="descending" select="res" />

        <tr>
          <xsl:choose>
            <xsl:when test="(res > /Root/Batch/Settings/redalert)">
              <TD align="center" class="colitemredalert">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(res)"/>
              </TD>
              <TD align="center" class="colitemredalert">-</TD>
              <TD align="center" class="colitemredalert">-</TD>
              <TD align="center" class="colitemredalert">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(name)"/>
              </TD>
              <TD align="center" class="colitemredalert">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(street)"/>
              </TD>
              <TD align="center" class="colitemredalert">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(city)"/>
              </TD>
              <TD align="center" class="colitemredalert">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(zip)"/>
              </TD>
              <TD align="center" class="colitemredalert">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(state)"/>
              </TD>
              <TD align="center" class="colitemredalert">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(country)"/>
              </TD>
              <TD align="center" class="colitemredalert">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(dentype)"/>
              </TD>
              <TD align="center" class="colitemredalert">
                <xsl:value-of select="DominoFormat:GetDateTime(date,'d')"/>
              </TD>
              <TD align="center" class="colitemredalert">
                <xsl:choose>
                  <xsl:when test="(sourceurl != '')">
                    <a>
                      <xsl:attribute name="href">
                        <xsl:value-of select="sourceurl"/>
                      </xsl:attribute>
                      <xsl:attribute name="target">_new</xsl:attribute>
                      <xsl:value-of select="DominoFormat:ReplaceEmpty(notes)"/>
                    </a>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="DominoFormat:ReplaceEmpty(notes)"/>
                  </xsl:otherwise>
                </xsl:choose>
              </TD>
            </xsl:when>
            <xsl:otherwise>
              <TD align="center" class="colitem">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(res)"/>
              </TD>
              <TD align="center" class="colitem">-</TD>
              <TD align="center" class="colitem">-</TD>
              <TD align="center" class="colitem">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(name)"/>
              </TD>
              <TD align="center" class="colitem">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(street)"/>
              </TD>
              <TD align="center" class="colitem">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(city)"/>
              </TD>
              <TD align="center" class="colitem">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(zip)"/>
              </TD>
              <TD align="center" class="colitem">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(state)"/>
              </TD>
              <TD align="center" class="colitem">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(country)"/>
              </TD>
              <TD align="center" class="colitem">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(dentype)"/>
              </TD>
              <TD align="center" class="colitem">
                <xsl:value-of select="DominoFormat:GetDateTime(date,'d')"/>
              </TD>
              <TD align="center" class="colitem">
                <xsl:choose>
                  <xsl:when test="(sourceurl != '')">
                    <a>
                      <xsl:attribute name="href">
                        <xsl:value-of select="sourceurl"/>
                      </xsl:attribute>
                      <xsl:attribute name="target">_new</xsl:attribute>
                      <xsl:value-of select="DominoFormat:ReplaceEmpty(notes)"/>
                    </a>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="DominoFormat:ReplaceEmpty(notes)"/>
                  </xsl:otherwise>
                </xsl:choose>
              </TD>
            </xsl:otherwise>
          </xsl:choose>
        </tr>

      </xsl:for-each>

      <tr>
        <TD vAlign="top" bgColor="#4F8EB6" colspan="12" height="3">
          <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
        </TD>
      </tr>
      <tr>
        <td colspan="12" height="15">
          <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
        </td>
      </tr>

    </xsl:if>

  </xsl:template>



  <xsl:template match="/Root/Tran/Src">
    <xsl:variable name="tranid" select="tranid" />

    <xsl:if test="/Root/Tran/Res[tranid=$tranid]">



      <xsl:for-each select="/Root/Tran/Res[tranid=$tranid]">
        <xsl:sort data-type="number" order="descending" select="res" />

        <tr>
          <xsl:choose>
            <xsl:when test="(res > /Root/Tran/Settings/redalert)">
              <TD align="center" class="colitemredalert">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(res)"/>
              </TD>
              <TD align="center" class="colitemredalert">-</TD>
              <TD align="center" class="colitemredalert">-</TD>
              <TD align="center" class="colitemredalert">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(name)"/>
              </TD>
              <TD align="center" class="colitemredalert">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(street)"/>
              </TD>
              <TD align="center" class="colitemredalert">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(city)"/>
              </TD>
              <TD align="center" class="colitemredalert">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(zip)"/>
              </TD>
              <TD align="center" class="colitemredalert">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(state)"/>
              </TD>
              <TD align="center" class="colitemredalert">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(country)"/>
              </TD>
              <TD align="center" class="colitemredalert">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(dentype)"/>
              </TD>
              <TD align="center" class="colitemredalert">
                <xsl:value-of select="DominoFormat:GetDateTime(date,'d')"/>
              </TD>
              <TD align="center" class="colitemredalert">
                <xsl:choose>
                  <xsl:when test="(sourceurl != '')">
                    <a>
                      <xsl:attribute name="href">
                        <xsl:value-of select="sourceurl"/>
                      </xsl:attribute>
                      <xsl:attribute name="target">_new</xsl:attribute>
                      <xsl:value-of select="DominoFormat:ReplaceEmpty(notes)"/>
                    </a>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="DominoFormat:ReplaceEmpty(notes)"/>
                  </xsl:otherwise>
                </xsl:choose>
              </TD>
            </xsl:when>
            <xsl:otherwise>
              <TD align="center" class="colitem">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(res)"/>
              </TD>
              <TD align="center" class="colitem">-</TD>
              <TD align="center" class="colitem">-</TD>
              <TD align="center" class="colitem">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(name)"/>
              </TD>
              <TD align="center" class="colitem">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(street)"/>
              </TD>
              <TD align="center" class="colitem">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(city)"/>
              </TD>
              <TD align="center" class="colitem">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(zip)"/>
              </TD>
              <TD align="center" class="colitem">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(state)"/>
              </TD>
              <TD align="center" class="colitem">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(country)"/>
              </TD>
              <TD align="center" class="colitem">
                <xsl:value-of select="DominoFormat:ReplaceEmpty(dentype)"/>
              </TD>
              <TD align="center" class="colitem">
                <xsl:value-of select="DominoFormat:GetDateTime(date,'d')"/>
              </TD>
              <TD align="center" class="colitem">
                <xsl:choose>
                  <xsl:when test="(sourceurl != '')">
                    <a>
                      <xsl:attribute name="href">
                        <xsl:value-of select="sourceurl"/>
                      </xsl:attribute>
                      <xsl:attribute name="target">_new</xsl:attribute>
                      <xsl:value-of select="DominoFormat:ReplaceEmpty(notes)"/>
                    </a>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="DominoFormat:ReplaceEmpty(notes)"/>
                  </xsl:otherwise>
                </xsl:choose>
              </TD>
            </xsl:otherwise>
          </xsl:choose>
        </tr>

      </xsl:for-each>

      <tr>
        <TD vAlign="top" bgColor="#4F8EB6" colspan="12" height="3">
          <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
        </TD>
      </tr>
      <tr>
        <td colspan="12" height="15">
          <DIV style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 1px; PADDING-TOP: 0px"></DIV>
        </td>
      </tr>

    </xsl:if>

  </xsl:template>



</xsl:stylesheet>
