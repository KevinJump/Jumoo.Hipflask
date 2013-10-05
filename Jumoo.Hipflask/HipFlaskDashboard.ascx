<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HipFlaskDashboard.ascx.cs" Inherits="Jumoo.Hipflask.HipFlaskDashboard" %>

<div class="propertypane">
    <div class="propertyItem">
        <div class="dashboardWrapper">
            <h2>Hipflask</h2>
            <img src="./dashboard/images/ZipFile.png" alt="Hipflask" class="dashboardIcon">
            <p>Get hip things for your umbraco install direct from the internet</p>
            <p>Hipflask downloads directly from the internet. so they are as upto date as they are</p>
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="updateCheck" runat="server" Visible="false">
                        <div style="background-color: #d74040; padding:1em; font-size:1em; margin: 2em;">
                            An update is avalible : <asp:Button ID="update" runat="server" Text="Update" OnClick="update_Click"/>
                        </div>
                    </asp:Panel>
                    <div class="dashboardColWrapper">
                        <div class="dashboardCols">
                            <div class="dashboardCol">
                                <ul>
                                <asp:ListView ID="HipsterList" runat="server" OnItemCommand="HipsterList_ItemCommand">
                                <ItemTemplate>
                                    <li>
                                        <strong><asp:LinkButton runat="server" 
                                            ID="SelectHipster" 
                                            Text="<%# Container.DataItem %>" 
                                            CommandName="DoHipStuff" 
                                            CommandArgument='<%# Container.DataItem %>' />
                                        </strong>
                                    </li>
                                </ItemTemplate>
                                </asp:ListView>
                                </ul>
                             </div>
                        </div>
                    </div>
                <asp:Label ID="Status" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                   <img text="Loading..." src="../umbraco_client/images/progressBar.gif">
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
</div>

