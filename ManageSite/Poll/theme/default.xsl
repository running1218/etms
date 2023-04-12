<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output method="html"/>
  <!-- 主模版定义 -->
  <xsl:template match="/">
    <xsl:apply-templates select="Query"></xsl:apply-templates>
  </xsl:template>
  <!-- 子模版定义项 -->
  <xsl:template match="Query">
    <!-- 定义问卷答案模版XML路径 -->
    <xml id="AnswerXml" style="display:none">
      <xsl:value-of select="AnswerXmlURL"/>
    </xml>
    <!-- 问卷框架 -->
    <div id="divQuery" class="thirdPageCon">
      <!-- 问卷描述信息 -->
      <div id="divQueryName" class="infoTit">
        <ul class="infoTitUl">
          <!-- 问卷主题 -->
          <li class="infoMainTit">
            <xsl:value-of select="QueryName"/>
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
      <div class="rightBottom">
        <ul>
          <!-- 结束语 -->
          <li class="infoComments">
            <xsl:value-of select="Footer"/>
          </li>
        </ul>
      </div>
      <div class="okDiv" id="divButton">
        <input type="button" value="提交" class="btn_Save" onclick="postDataToServer();"/>
        <input type="button" value="重置" class="btn_Cancel" onclick="document.forms[0].reset();"/>
      </div>

      <script language="javascript">
        //预览模式下不显示提交/重置按钮
        if(!SwitchPreViewMode())
        {
        LoadAnswerXML();
        }

      </script>
    </div>
    <!-- 提示信息 -->
    <div id="divMsg" style="display:none"  class="thirdPageCon">
      <div class="infoTit">
        <ul class="infoTitUl">
          <li class="infoMainTit">
            <xsl:text>提示：</xsl:text>
            <label id="msgWin"></label>
          </li>
          <li class="infoSubTit"></li>
        </ul>
      </div>
      <div class="okDiv" id="divButton">
        <input type="button" value="关闭" class="btn_Close" onclick="javascript:window.close();"/>
      </div>
      <div class="rightBottom"></div>
    </div>
    <!-- 交卷后提示信息 -->
    <div id="divFooter" style="display:none"  class="thirdPageCon">
      <div class="infoTit">
        <ul class="infoTitUl">
          <li class="infoMainTit">
            <xsl:text>提示：</xsl:text>
            <xsl:choose>
              <xsl:when test="Footer=''">调查结束，感谢您的支持！</xsl:when>
              <xsl:otherwise>
                <xsl:value-of select="Footer"/>
              </xsl:otherwise>
            </xsl:choose>
          </li>
          <li class="infoSubTit"></li>
        </ul>
      </div>
      <xsl:choose>
        <xsl:when test="//IsDisplayResult=1">
          <div class="okDiv" id="divButton">
            <input type="button" value="查看" class="btn_2" onclick="ViewStatResult();"/>
            <input type="button" value="关闭" class="btn_Close" onclick="javascript:window.close();"/>
          </div>
        </xsl:when>
        <xsl:otherwise>
          <div class="okDiv" id="divButton">
            <input type="button" value="关闭" class="btn_Close" onclick="javascript:window.close();"/>
          </div>
        </xsl:otherwise>
      </xsl:choose>
      <div class="rightBottom"></div>
    </div>
    <!-- 在页面内容完全加载后，置问卷默认显示 -->
    <script language="javascript">
      SwitchDivVisble("divQuery");
    </script>
  </xsl:template>
  <!-- 小题模版 -->
  <xsl:template name="Title">
    <ul id="divTitle{TitleID}" class="surveyUl">
      <li>
        <span class="surveyTit">
          <!-- 问题描述 -->
          <xsl:value-of select="/Query/TitlePrefix"></xsl:value-of><xsl:value-of select="position()"></xsl:value-of>. <xsl:value-of select="TitleName"/><label id="lblTitle_{TitleID}" style="display:none;color:red">*</label>
        </span>
      </li>
      <!-- 单项选择题型 -->
      <xsl:if test="TitleTypeID = 1 ">
        <xsl:call-template name="SigleSelect"></xsl:call-template>
      </xsl:if>
      <!-- 多项选择题型 -->
      <xsl:if test="TitleTypeID = 2 ">
        <xsl:call-template name="MutilSelect"></xsl:call-template>
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
  <!-- 单项选择题模版 -->
  <xsl:template name="SigleSelect">
    <xsl:for-each select=".//Option">
      <xsl:choose>
        <xsl:when test=" OtherLength = 0 ">
          <li>
            <span class="inputSpan">
              <xsl:number value="position()" format="A"/>.
            </span>
            <input type="radio" class="inputCircle"  name="{TitleID}" TitleID="{TitleID}" OptionID="{OptionID}" TitleType="1" IsOther="0"  onclick="save(this)"/>
            <xsl:value-of select="OptionName"/>
          </li>
        </xsl:when>
        <xsl:otherwise>
          <li>
            <span class="inputSpan">
              <xsl:number value="position()" format="A"/>.
            </span>
            <input type="radio" class="inputCircle"  name="{TitleID}" id="rboption_{OptionID}"  TitleID="{TitleID}" OptionID="{OptionID}"  TitleType="1" IsOther="1" onclick="txtoption_{OptionID}.readOnly=false;txtoption_{OptionID}.value='';save(this);" onblur="txtoption_{OptionID}.readOnly=true;" />
            <xsl:value-of select="OptionName"/>
            <input type="text" class="inputbox_300" maxlength="100" id="txtoption_{OptionID}" readonly="true" onclick="if(rboption_{OptionID}.checked)this.readOnly=false; else this.readOnly=true;"  onchange="save(document.getElementById('rboption_{OptionID}'));" />(100字以内)
          </li>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:for-each>
  </xsl:template>
  <!-- 多项选择题模版 -->
  <xsl:template name="MutilSelect">
    <xsl:for-each select=".//Option">
      <xsl:choose>
        <xsl:when test=" OtherLength = 0 ">
          <li>
            <span class="inputSpan">
              <xsl:number value="position()" format="A"/>.
            </span>
            <input type="checkbox"    class="inputCircle" name="{TitleID}" TitleID="{TitleID}" OptionID="{OptionID}" TitleType="2" IsOther="0" onclick="save(this);"/>
            <xsl:value-of select="OptionName"/>
          </li>
        </xsl:when>
        <xsl:otherwise>
          <li>
            <span  class="inputSpan">
              <xsl:number value="position()" format="A"/>.
            </span>
            <input type="checkbox"  class="inputCircle" name="{TitleID}" id="rboption_{OptionID}" TitleID="{TitleID}" OptionID="{OptionID}" TitleType="2" IsOther="1" onclick="txtoption_{OptionID}.readOnly=false;txtoption_{OptionID}.value='';save(this);" onblur="txtoption_{OptionID}.readOnly=true;" />
            <xsl:value-of select="OptionName"/>
            <input type="text" class="inputbox_300" maxlength="100" id="txtoption_{OptionID}" readonly="true"  onclick="if(rboption_{OptionID}.checked)this.readOnly=false; else this.readOnly=true;" onchange="save(document.getElementById('rboption_{OptionID}'));" />(100字以内)
          </li>
        </xsl:otherwise>
      </xsl:choose>
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
            <td>
              <input type="radio" class="inputCircle" name="rdmatrix_{$currentOption}" TitleID="{TitleID}"  OptionID="{$currentOption}" TitleType="3" HeaderID="{HeaderID}" onclick="save(this);"></input>
            </td>
          </xsl:for-each>
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>
  <!-- 简答题模版 -->
  <xsl:template name="Text">
    <li>
      <textarea class="inputbox_area440" TitleID="{TitleID}" TitleType="4" onchange="save(this);"></textarea>
    </li>
  </xsl:template>
</xsl:stylesheet>
