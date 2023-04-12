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
    <div id="divQuery"  class="thirdPageCon">
      <!-- 问卷描述信息 -->
      <div id="divQueryName" class="infoTit">
        <ul class="infoTitUl">
          <!-- 问卷主题 -->
          <li class="infoMainTit">
            <xsl:value-of select="QueryName"/>
            <!-- 参与调查人数 -->
            <xsl:text>（共：</xsl:text>
            <xsl:value-of select="UserCounts" />
            <xsl:text>人参与调查）</xsl:text>
          </li>
          <!-- 问卷说明 -->
          <li class="infoComments">
            <xsl:value-of select="Header"/>
          </li>
        </ul>
      </div>
      <xsl:choose>
        <xsl:when test="IsDisplayColumn = 1">
          <!-- 按栏目列表展现 -->
          <xsl:for-each select="Columns/Column">
            <div class="survey">
              <!-- 栏目名称 -->
              <h1>
                <xsl:value-of select="ColumnName"/>
              </h1>
              <!-- 栏目下的题列表 -->
              <xsl:for-each select=".//Title">
                <xsl:call-template name="Title" />
              </xsl:for-each>
            </div>
          </xsl:for-each>
        </xsl:when>
        <xsl:otherwise>
          <div class="survey">
            <!-- 按题列表展现 -->
            <xsl:for-each select="//Title">
              <xsl:call-template name="Title" />
            </xsl:for-each>
          </div>
        </xsl:otherwise>
      </xsl:choose>
      <div class="okDiv" id="divButton">
        <input type="submit" value="导出" class="btn_Export" />
        <input type="button" value="关闭" class="btn_Close" onclick="javascript:window.close();"/>
      </div>
      <div class="rightBottom"></div>
    </div>
  </xsl:template>
  <xsl:template name="Title">
    <ul id="divTitle{TitleID}" class="surveyUl">
      <li>
        <!-- 问题描述 -->
        <span class="surveyTit">
          <xsl:value-of select="/Query/TitlePrefix"></xsl:value-of><xsl:value-of select="position()"></xsl:value-of>. <xsl:value-of select="TitleName"/>
        </span>
      </li>
      <!-- 单项选择题型 -->
      <xsl:if test="TitleTypeID = 1 ">
        <xsl:call-template name="Select"></xsl:call-template>
      </xsl:if>
      <!-- 多项选择题型 -->
      <xsl:if test="TitleTypeID = 2 ">
        <xsl:call-template name="MoreSelect"></xsl:call-template>
      </xsl:if>
      <!-- 矩阵题型 -->
      <xsl:if test="TitleTypeID = 3 ">
        <xsl:call-template name="MatrixSelect"></xsl:call-template>
      </xsl:if>
      <!-- 简答题型 -->
      <xsl:if test="TitleTypeID = 4 ">
        <xsl:call-template name="Text"></xsl:call-template>
      </xsl:if>
    </ul>
  </xsl:template>
  <!-- 单/多选择题模版 -->
  <xsl:template name="Select">
    <xsl:variable name="SumOptionNums" select="sum(.//OptionNums)"></xsl:variable> 
    <xsl:for-each select=".//Option">
      <li>
        <xsl:if test=" position()=last()">
          <xsl:attribute name="style">border-bottom:none;</xsl:attribute>
        </xsl:if>
        <h1>
          <xsl:number value="position()" format="A"/>.<xsl:value-of select="OptionName"/>
        </h1>
        <xsl:choose>
          <xsl:when test="$SumOptionNums = 0">
            <div class="diaochaProgress">
              <div class="diaochaProgressBgLeft"></div>
              <div class="diaochaProgressBg"></div>
              <div class="diaochaProgressBgRight"></div>
              <div class="tipProgress">[0%]0人</div>
              <div class="diaochaProgressBgShow">
                <div class="diaochaProgressGreenLeft"></div>
                <div class="diaochaProgressGreenBg" style="width:0px"></div>
                <div class="diaochaProgressGreenRight"></div>
              </div>
            </div>
          </xsl:when>
          <xsl:otherwise>
            <xsl:variable name="percent" select="format-number(number(OptionNums) div $SumOptionNums * 100, '##.00')"/>
            <xsl:variable name="subpercent" select="100-$percent"/>
            <div class="diaochaProgress">
              <div class="diaochaProgressBgLeft"></div>
              <div class="diaochaProgressBg"></div>
              <div class="diaochaProgressBgRight"></div>
              <div class="tipProgress">
                <xsl:choose>
                  <xsl:when test="starts-with($percent,'.')">
                    <xsl:text>[0%]</xsl:text>
                  </xsl:when>
                  <xsl:when test="contains($percent,'.00')">
                    [<xsl:value-of select="substring-before($percent,'.')"/>%]
                  </xsl:when>
                  <xsl:otherwise>
                    [<xsl:value-of select="$percent"></xsl:value-of>%]
                  </xsl:otherwise>
                </xsl:choose>
                <xsl:value-of select="OptionNums"/>
                <xsl:text>人</xsl:text>
              </div>
              <div class="diaochaProgressBgShow">
                <div class="diaochaProgressGreenLeft"></div>
                <div class="diaochaProgressGreenBg" style="width:{$percent * 4.96}px"></div>
                <div class="diaochaProgressGreenRight"></div>
              </div>
            </div>
            <div class="diaochaOther">
              <xsl:for-each select=".//AnswerItem">
                其他<xsl:number value="position()" format="1"/>：<xsl:value-of select="Answer"/>
                <br/>
              </xsl:for-each>
            </div>
          </xsl:otherwise>
        </xsl:choose>
      </li>
    </xsl:for-each>
  </xsl:template>
  <xsl:template name="MoreSelect">
    <xsl:variable name="SumOptionNums" select="sum(.//OptionNums)"></xsl:variable>    
    <xsl:for-each select=".//Option">
      <li>
        <xsl:if test=" position()=last()">
          <xsl:attribute name="style">border-bottom:none;</xsl:attribute>
        </xsl:if>
        <h1>
          <xsl:number value="position()" format="A"/>.<xsl:value-of select="OptionName"/>
        </h1>
        <xsl:choose>
          <xsl:when test="$SumOptionNums = 0">
            <div class="diaochaProgress">
              <div class="diaochaProgressBgLeft"></div>
              <div class="diaochaProgressBg"></div>
              <div class="diaochaProgressBgRight"></div>
              <div class="tipProgress">[0%]0人</div>
              <div class="diaochaProgressBgShow">
                <div class="diaochaProgressGreenLeft"></div>
                <div class="diaochaProgressGreenBg" style="width:0px"></div>
                <div class="diaochaProgressGreenRight"></div>
              </div>
            </div>
          </xsl:when>
          <xsl:otherwise>
            <xsl:variable name="percent" select="format-number( 100 div AnswerNum * OptionNums , '##.00')"/>
            <xsl:variable name="subpercent" select="100-$percent"/>
            <div class="diaochaProgress">
              <div class="diaochaProgressBgLeft"></div>
              <div class="diaochaProgressBg"></div>
              <div class="diaochaProgressBgRight"></div>
              <div class="tipProgress">
                <xsl:choose>
                  <xsl:when test="starts-with($percent,'.')">
                    <xsl:text>[0%]</xsl:text>
                  </xsl:when>
                  <xsl:when test="contains($percent,'.00')">
                    [<xsl:value-of select="substring-before($percent,'.')"/>%]
                  </xsl:when>
                  <xsl:otherwise>
                    [<xsl:value-of select="$percent "></xsl:value-of>%]
                  </xsl:otherwise>
                </xsl:choose>
                <xsl:value-of select="OptionNums"/>
                <xsl:text>人</xsl:text>
              </div>
              <div class="diaochaProgressBgShow">
                <div class="diaochaProgressGreenLeft"></div>
                <div class="diaochaProgressGreenBg" style="width:{$percent * 4.96}px"></div>
                <div class="diaochaProgressGreenRight"></div>
              </div>
            </div>
            <div class="diaochaOther">
              <xsl:for-each select=".//AnswerItem">
                其他<xsl:number value="position()" format="1"/>：<xsl:value-of select="Answer"/>
                <br/>
              </xsl:for-each>
            </div>
          </xsl:otherwise>
        </xsl:choose>
      </li>
    </xsl:for-each>
  </xsl:template>
  <!-- 矩阵题模版 -->
  <xsl:template name="MatrixSelect">
    <table width="90%" border="0" cellspacing="0" cellpadding="0" class="surveyTab">
      <thead>
        <tr>
          <th>&#127;</th>
          <xsl:for-each select=".//Header">
            <th>
              <xsl:value-of select="HeaderName"/>
            </th>
          </xsl:for-each>
          <th>行统计</th>
        </tr>
      </thead>
      <xsl:for-each select=".//Option">
        <xsl:variable name="currentOption" select="OptionID"></xsl:variable>
        <tr>
          <xsl:choose>
            <xsl:when test="position() mod 2 = 0">
              <xsl:attribute name="class" >list_b</xsl:attribute>
            </xsl:when>
            <xsl:otherwise>
              <xsl:attribute name="class" >list_a</xsl:attribute>
            </xsl:otherwise>
          </xsl:choose>
          <th>
            <xsl:number value="position()" format="1"/>.
            <xsl:value-of select="OptionName"/>
          </th>
          <xsl:for-each select="../../Headers/Header">
            <xsl:variable name="currentHeaderId" select="HeaderID"></xsl:variable>
            <td>
              <xsl:value-of select="//DataItem[@OptionID=$currentOption and @HeaderID=$currentHeaderId]/@Num"/>
            </td>
          </xsl:for-each>
          <td>
            <xsl:value-of select="OptionNums"/>
          </td>
        </tr>
      </xsl:for-each>
      <tr>
        <th>列统计</th>
        <xsl:for-each select=".//Header">
          <xsl:variable name="currentHeaderId" select="HeaderID"></xsl:variable>
          <td>
            <xsl:value-of  select="sum(//DataItem[ @HeaderID=$currentHeaderId]/@Num)"/>
          </td>
        </xsl:for-each>
        <td></td>
      </tr>
    </table>
  </xsl:template>
  <!-- 简答题模版 -->
  <xsl:template name="Text">
    <xsl:variable name="AnswerItemCount" select="count(.//AnswerItem)"></xsl:variable>
    <xsl:for-each select=".//AnswerItem">
      <li>
        <xsl:if test=" AnswerItemCount=last()">
          <xsl:attribute name="style">border-bottom:none;</xsl:attribute>
        </xsl:if>
        <h1>
          <xsl:text>答复</xsl:text>
          <xsl:number value="position()" format="1"/>
          <xsl:text>：</xsl:text>
          <!--<xsl:choose>
            <xsl:when test="string-length(Answer) > 50">
              <xsl:value-of select="substring(Answer,1,50)"/>
              <xsl:text>。。。</xsl:text>
            </xsl:when>
            <xsl:otherwise>-->
              <xsl:value-of select="Answer"/>
            <!--</xsl:otherwise>
          </xsl:choose>-->
        </h1>
      </li>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>
