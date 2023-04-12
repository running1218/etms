<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:output method="html"/>
	<!-- 主模版定义 -->
	<xsl:template match="/">
		<xsl:apply-templates select="Query"></xsl:apply-templates>
	</xsl:template>
	<!-- 子模版定义项 -->
	<xsl:template match="Query">
		<!-- 问卷提交页面主JS文件，说明：$Root$将在XML转换为HTML时动态替换 -->
		<script language="javascript" src="$Root$/JScript/Inquiry.js"></script>
		<!-- 定义问卷答案模版XML路径 -->
		<script language="javascript" >
			var URL='<xsl:value-of select="AnswerXmlURL"/>';
			//异步载入用户问卷答案
			LoadAnswerXMLForDisplay();
		</script>
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
			<div class="okDiv" id="divButton">
				<input type="button" value="关　闭" class="btn4" onclick="javascript:window.close();"/>
			</div>
			<div class="rightBottom"></div>
		</div>	 
	</xsl:template>
	<!-- 小题模版 -->
	<xsl:template name="Title">
		<ul id="divTitle{TitleID}" class="surveyUl">
			<li>
				<span class="surveyTit">
					<!-- 问题描述 -->
					<xsl:value-of select="/Query/TitlePrefix"></xsl:value-of><xsl:value-of select="position()"></xsl:value-of>. <xsl:value-of select="TitleName"/>
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
						<xsl:number value="position()" format="A"/>.
						<input type="radio" class="inputCircle"  name="{TitleID}" TitleID="{TitleID}" OptionID="{OptionID}"/>
						<xsl:value-of select="OptionName"/>
					</li>
				</xsl:when>
				<xsl:otherwise>
					<li>
						<xsl:number value="position()" format="A"/>.
						<input type="radio" class="inputCircle"  name="{TitleID}" id="rboption_{OptionID}"  TitleID="{TitleID}" OptionID="{OptionID}" />
						<xsl:value-of select="OptionName"/><xsl:text>&#127;&#127;</xsl:text>
						<input type="text" class="inputBlueShort" maxlength="100" id="txtoption_{OptionID}" readonly="true" />(100字以内)
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
						<xsl:number value="position()" format="A"/>.
						<input type="checkbox"  class="inputCircle" name="{TitleID}" TitleID="{TitleID}" OptionID="{OptionID}"/>
						<xsl:value-of select="OptionName"/>
					</li>
				</xsl:when>
				<xsl:otherwise>
					<li>
						<xsl:number value="position()" format="A"/>.
						<input type="checkbox" class="inputCircle" name="{TitleID}" id="rboption_{OptionID}" TitleID="{TitleID}" OptionID="{OptionID}"/>
						<xsl:value-of select="OptionName"/><xsl:text>&#127;&#127;</xsl:text>
						<input type="text" class="inputBlueShort" maxlength="100" id="txtoption_{OptionID}" readonly="true"  />(100字以内)
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
							<input type="radio" class="inputCircle" name="rdmatrix_{$currentOption}" TitleID="{TitleID}"  OptionID="{$currentOption}" HeaderID="{HeaderID}"></input>
						</td>
					</xsl:for-each>
				</tr>
			</xsl:for-each>
		</table>
	</xsl:template>
	<!-- 简答题模版 -->
	<xsl:template name="Text">
		<li>
			<textarea cols="45" rows="5" class="inputBlueTextArea_survey" TitleID="{TitleID}" TitleType="4"></textarea>
		</li>
	</xsl:template>
</xsl:stylesheet>
