<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output method="html"/>
  <!-- 主模版定义 -->
  <xsl:template match="/">
    <xsl:apply-templates select="Query"></xsl:apply-templates>
  </xsl:template>
  <!-- 子模版定义项 -->
  <xsl:template match="Query">
    <!-- 问卷框架 -->
    <table width="100%" border="1">
      <tr>
        <td align="center">
          <h3>
            <!-- 问卷主题 -->
            <xsl:value-of select="QueryName"/>
            <!-- 参与调查人数 -->
            <xsl:text>（共：</xsl:text>
            <xsl:value-of select="UserCounts" />
            <xsl:text>人参与调查）</xsl:text>
          </h3>
        </td>
      </tr>
      <tr>
        <td style="height: 25px;" valign="middle">
          <!-- 问卷说明 -->
          <xsl:value-of select="Header"/>
        </td>
      </tr>
      <xsl:choose>
        <xsl:when test="IsDisplayColumn = 1">
          <!-- 按栏目列表展现 -->
          <xsl:for-each select="Columns/Column">
            <tr>
              <td>
                <!-- 栏目名称 -->
                <xsl:value-of select="ColumnName"/>
              </td>
            </tr>
            <!-- 栏目下的题列表 -->
            <xsl:for-each select=".//Title">
              <tr>
                <td style="padding:5px 0px 5px 40px;">
                  <xsl:call-template name="Title" />
                </td>
              </tr>
            </xsl:for-each>
          </xsl:for-each>
        </xsl:when>
        <xsl:otherwise>
          <xsl:for-each select="//Title">
            <tr>
              <td style="padding:5px 0px 5px 0px;">
                <!-- 按题列表展现 -->
                <xsl:call-template name="Title" />
              </td>
            </tr>
          </xsl:for-each>
        </xsl:otherwise>
      </xsl:choose>
    </table>
  </xsl:template>
  <xsl:template name="Title">
    <!-- 问题描述 -->
    <xsl:value-of select="/Query/TitlePrefix"></xsl:value-of><xsl:value-of select="position()"></xsl:value-of>. <xsl:value-of select="TitleName"/>
    <xsl:variable name="StyleControl" select="//Query/IsDisplayColumn"></xsl:variable>
    <table border="1">
      <!-- 单项选择题型 -->
      <xsl:if test="TitleTypeID = 1 ">
        <xsl:call-template name="Select"></xsl:call-template>
      </xsl:if>
      <!-- 多项选择题型 -->
      <xsl:if test="TitleTypeID = 2 ">
        <xsl:call-template name="Select"></xsl:call-template>
      </xsl:if>
      <!-- 矩阵题型 -->
      <xsl:if test="TitleTypeID = 3 ">
        <xsl:call-template name="MatrixSelect"></xsl:call-template>
      </xsl:if>
      <!-- 简答题型 -->
      <xsl:if test="TitleTypeID = 4 ">
        <xsl:call-template name="Text"></xsl:call-template>
      </xsl:if>
    </table>

  </xsl:template>
  <!-- 单/多选择题模版 -->
  <xsl:template name="Select">
    <xsl:for-each select=".//Option">
      <tr>
        <td>
          <xsl:if test="//Query/IsDisplayColumn = 1 ">
            <xsl:attribute name="style">padding:0 0 0 30px;</xsl:attribute>
          </xsl:if>
          <xsl:if test="//Query/IsDisplayColumn = 0">
            <xsl:attribute name="style">padding:0 0 0 20px;</xsl:attribute>
          </xsl:if>
          <!-- 答案选项 -->
          <xsl:number value="position()" format="A"/>.<xsl:value-of select="OptionName"/>
          <xsl:for-each select=".//AnswerItem">
            <br/><xsl:value-of select="Answer"/>
          </xsl:for-each>
        </td>
        <td width="10%">
          <xsl:value-of select="OptionNums"/>
        </td>
      </tr>
    </xsl:for-each>
  </xsl:template>
  <!-- 矩阵题模版 -->
  <xsl:template name="MatrixSelect">
    <tr>
      <td>
        <xsl:if test="//Query/IsDisplayColumn = 1 ">
          <xsl:attribute name="style">padding:0 0 0 30px;</xsl:attribute>
        </xsl:if>
        <xsl:if test="//Query/IsDisplayColumn = 0">
          <xsl:attribute name="style">padding:0 0 0 20px;</xsl:attribute>
        </xsl:if>
        <table width="90%" border="1">
          <tr>
            <td >
              <xsl:text> </xsl:text>
            </td>
            <xsl:for-each select=".//Header">
              <td align="center">
                <xsl:value-of select="HeaderName"/>
              </td>
            </xsl:for-each>
            <td align="right">行统计</td>
          </tr>
          <xsl:for-each select=".//Option">
            <xsl:variable name="currentOption" select="OptionID"></xsl:variable>
            <tr>
              <td height="20px" >
                <xsl:number value="position()" format="1"/>.
                <xsl:value-of select="OptionName"/>
              </td>
              <xsl:for-each select="../../Headers/Header">
                <xsl:variable name="currentHeaderId" select="HeaderID"></xsl:variable>
                <td width="7%" align="center">
                  <xsl:value-of select="//DataItem[@OptionID=$currentOption and @HeaderID=$currentHeaderId]/@Num"/>
                </td>
              </xsl:for-each>
              <td width="5%" align="right">
                <xsl:value-of select="OptionNums"/>
              </td>
            </tr>
          </xsl:for-each>
          <tr>
            <td height="20px" align="center">列统计</td>
            <xsl:for-each select=".//Header">
              <xsl:variable name="currentHeaderId" select="HeaderID"></xsl:variable>
              <td  align="center">
                <xsl:value-of  select="sum(//DataItem[ @HeaderID=$currentHeaderId]/@Num)"/>
              </td>
            </xsl:for-each>
            <td></td>
          </tr>
        </table>
      </td>
    </tr>

  </xsl:template>
  <!-- 简答题模版 -->
  <xsl:template name="Text">
    <xsl:for-each select=".//AnswerItem">
      <tr>
        <td colspan="2">
          <xsl:if test="//Query/IsDisplayColumn = 1 ">
            <xsl:attribute name="style">padding:0 0 0 30px;</xsl:attribute>
          </xsl:if>
          <xsl:if test="//Query/IsDisplayColumn = 0">
            <xsl:attribute name="style">padding:0 0 0 20px;</xsl:attribute>
          </xsl:if>
          <xsl:text>答复</xsl:text>
          <xsl:number value="position()" format="1"/>
          <xsl:text>：</xsl:text>
          <xsl:value-of select="Answer"/>
        </td>
      </tr>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>
