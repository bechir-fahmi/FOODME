INSERT INTO [dbo].[WorkflowScheme] ([Code], [Scheme], [CanBeInlined], [InlinedSchemes], [Tags]) VALUES (N'afcoProcess', N'<Process Name="SimpleWF" CanBeInlined="false" Tags="" LogEnabled="false">
  <Designer />
  <Commands>
    <Command Name="go" />
    <Command Name="final" />
  </Commands>
  <Activities>
    <Activity Name="Receive Order" State="Order received" IsInitial="true" IsFinal="false" IsForSetState="true" IsAutoSchemeUpdate="true">
      <Implementation>
        <ActionRef Order="1" NameRef="CheckPriceA" />
      </Implementation>
      <Designer X="360" Y="120" Hidden="false" />
    </Activity>
    <Activity Name="Loyality" State="Loyality calculated" IsInitial="false" IsFinal="false" IsForSetState="true" IsAutoSchemeUpdate="true">
      <Designer X="660" Y="120" Hidden="false" />
    </Activity>
    <Activity Name="Taker" State="ready to deliver" IsInitial="false" IsFinal="true" IsForSetState="true" IsAutoSchemeUpdate="true">
      <Designer X="670" Y="390" Hidden="false" />
    </Activity>
  </Activities>
  <Transitions>
    <Transition Name="OrderToLoyality" To="Loyality" From="Receive Order" Classifier="Direct" AllowConcatenationType="And" RestrictConcatenationType="And" ConditionsConcatenationType="And" DisableParentStateControl="false">
      <Triggers>
        <Trigger Type="Command" NameRef="go" />
      </Triggers>
      <Conditions>
        <Condition Type="Action" NameRef="MyCondition" ConditionInversion="false" />
      </Conditions>
      <Designer Hidden="false" />
    </Transition>
    <Transition Name="LoyalityToTaker" To="Taker" From="Loyality" Classifier="Direct" AllowConcatenationType="And" RestrictConcatenationType="And" ConditionsConcatenationType="And" DisableParentStateControl="false">
      <Triggers>
        <Trigger Type="Command" NameRef="final" />
      </Triggers>
      <Conditions>
        <Condition Type="Always" />
      </Conditions>
      <Designer Hidden="false" />
    </Transition>
    <Transition Name="OrderToTaker" To="Taker" From="Receive Order" Classifier="Direct" AllowConcatenationType="And" RestrictConcatenationType="And" ConditionsConcatenationType="And" DisableParentStateControl="false">
      <Triggers>
        <Trigger Type="Command" NameRef="go" />
      </Triggers>
      <Conditions>
        <Condition Type="Action" NameRef="MyCondition" ConditionInversion="true" />
      </Conditions>
      <Designer X="387" Y="347" Hidden="false" />
    </Transition>
  </Transitions>
</Process>', 0, N'', N'')


delete from WorkflowApprovalHistory
delete from WorkflowGlobalParameter
delete from WorkflowInbox
delete from WorkflowProcessAssignment
delete from WorkflowProcessInstance
delete from WorkflowProcessInstancePersistence
delete from WorkflowProcessInstanceStatus
delete from WorkflowProcessScheme
delete from WorkflowProcessTimer
delete from WorkflowProcessTransitionHistory
delete from WorkflowRuntime
delete from WorkflowScheme
delete from WorkflowSync


select count (*) from WorkflowApprovalHistory
select count (*) from WorkflowGlobalParameter
select count (*) from WorkflowInbox
select count (*) from WorkflowProcessAssignment
select count (*) from WorkflowProcessInstance
select count (*) from WorkflowProcessInstancePersistence
select count (*) from WorkflowProcessInstanceStatus
select count (*) from WorkflowProcessScheme
select count (*) from WorkflowProcessTimer
select count (*) from WorkflowProcessTransitionHistory
select count (*) from WorkflowRuntime
select count (*) from WorkflowScheme
select count (*) from WorkflowSync