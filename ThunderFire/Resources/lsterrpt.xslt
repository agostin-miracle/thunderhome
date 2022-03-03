<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" 
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt" 
                exclude-result-prefixes="msxsl"
>
  <xsl:output method="html" indent="yes"/>  
  <xsl:template match="//messages">
    <html>
      <style>
        body {width:600px;}
        table {font-family: Arial,Univers,sans-serif;font-size:12pt;}
        table td {border:#000000 1px solid;font-family: Arial,Univers,sans-serif;font-size: 12pt }
        table th {border:#000000 1px solid;font-family: Arial,Univers,sans-serif;font-size: 12pt;color:#c0c0c0; }
        table caption {background-color:#e0e0e0;border:#c0c0c0 1px solid;}
      </style>
      <body>
        <table align="center">
          <caption>Tabela de Definição de Mensagens</caption>
          <tr>
            <th style="width:2%">#</th>
            <th style="width:12%">ID</th>
            <th style="width:12%">Número</th>
            <th style="width:12%">Severidade</th>
            <th>Mensagem</th>
          </tr>
          <xsl:for-each select="code">
            <xsl:sort select="concat(@number,'000000')" data-type="number"/>
            <xsl:sort select="severity" data-type="text"/>
            <tr>
              <xsl:attribute name ="style">
              <xsl:if test="(position() mod 2) = 1">
                <xsl:text>background-color: #ededed;</xsl:text>
              </xsl:if>
              </xsl:attribute>
              <td valign="top">
                <xsl:value-of select="position()"/>
              </td>

              <td valign="top">
                <xsl:value-of select="@id"/>
              </td>
              <td valign="top">
                <xsl:value-of select="@number"/>
              </td>
              <td valign="top">
                <xsl:value-of select="severity"/>
              </td>
              <td valign="top">
                <xsl:value-of select="message"/>
              </td>
            </tr>
            <xsl:if test="description!= ''">
            <tr>
              <xsl:attribute name ="style">
                <xsl:if test="(position() mod 2) = 1">
                  <xsl:text>background-color: #ededed;</xsl:text>
                </xsl:if>
              </xsl:attribute>
              <td colspan="5" valign="top" align="justify">
                <b>Descrição : </b>
                <xsl:value-of select="description"/>
              </td>
            </tr>
            </xsl:if>              
          </xsl:for-each>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>