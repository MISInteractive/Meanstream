<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:map="http://schemas.microsoft.com/AspNet/SiteMap-File-1.0" exclude-result-prefixes="map">
  <xsl:output method="xml" encoding="utf-8" indent="yes"/>
  <xsl:template name="mapNode" match="map:siteMap">
    <ul>
      <xsl:apply-templates/>
    </ul>
  </xsl:template>
  <xsl:template match="map:siteMapNode">
    <li>
      <a href="http://meanstream.com{substring(@url, 2)}" title="{@description}">
        <xsl:value-of select="@title"/>
      </a>
      <xsl:if test="map:siteMapNode">
        <xsl:call-template name="mapNode"/>
      </xsl:if>
    </li>
  </xsl:template>
</xsl:stylesheet>
