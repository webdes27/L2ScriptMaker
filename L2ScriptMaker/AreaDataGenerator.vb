Public Class AreaDataGenerator

    'area_begin name=[ssq_main_pvp_1721_01] map_no = {17;21} type=ssq_zone
    'exp_penalty_per=25 item_drop=off message_no = 686
    'default_status = off  event_id=2418008
    'damage_on_hp=200 damage_on_mp=0
    'range = {{-78713;110409;-4948;-4748};{-77923;110423;-4948;-4748};{-77923;110522;-4948;-4748};{-78708;110515;-4948;-4748}}
    'area_end

    Private Sub StartButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartButton.Click

        ' Check null value in areagrid
        Dim iTemp As Integer, iTemp2 As Integer
        If DataGrid.RowCount = 1 Then
            MessageBox.Show("Zone coorninates empty. Enter and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error)
            FinishDataBox.Text = ""
            Exit Sub
        End If
        For iTemp = 0 To DataGrid.RowCount - 2
            For iTemp2 = 0 To DataGrid.ColumnCount - 1
                If DataGrid.Item(iTemp2, iTemp).Value Is Nothing = True Then
                    MessageBox.Show("Empty data in line: " & iTemp + 1 & " column: " & iTemp2 + 1 & "! Edit and calculate again.", "Wrong chance", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                Try
                    CInt(DataGrid.Item(iTemp2, iTemp).Value).ToString()
                Catch ex As Exception
                    MessageBox.Show("Value in line: " & iTemp + 1 & " column: " & iTemp2 + 1 & " is not Digital. Enter valid value and try again.", "Wrong value", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Next
        Next


        ' ----------- Start Generation ------------------
        If AutoClearBox.Checked = True Then FinishDataBox.Text = ""
        FinishDataBox.Text += "// " & Now & ", area zone generated by L2ScriptMaker: AreaData Generator" & vbNewLine

        ' Area Comment
        If AreaCommentBox.Text <> "" Then
            FinishDataBox.Text += "// " & AreaCommentBox.Text & vbNewLine
        End If

        FinishDataBox.Text += "area_begin"
        ' Area Name
        If ZoneNameBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "name=[" & ZoneNameBox.Text.Replace(" ", "_").ToLower & "]"
        Else
            MessageBox.Show("Zone name cant be empty. Enter and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error)
            FinishDataBox.Text += vbNewLine
            Exit Sub
        End If
        ' Area Map_no
        FinishDataBox.Text += vbTab & "map_no={" & MapNo1Box.Text & ";" & MapNo2Box.Text & "}"
        ' Area Type
        If AreaTypeBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "type=" & AreaTypeBox.Text
        Else
            MessageBox.Show("Zone Type cant be empty. Select from list and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error)
            FinishDataBox.Text += vbNewLine
            Exit Sub
        End If
        ' target
        If TypeTargetBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "target=" & TypeTargetBox.Text
        End If

        'skill_name=[s_area_fire1] 
        If SkillNameBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "skill_name=[" & SkillNameBox.Text & "]"
        End If

        'skill_list={@s_area_poison7;@s_area_pd_down3} skill_action_type=serial
        If SkillListBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "skill_list={"
            For iTemp = 0 To SkillListBox.Lines.Length - 1
                If iTemp > 0 And iTemp < SkillListBox.Lines.Length Then
                    FinishDataBox.Text += ";"
                End If
                FinishDataBox.Text += "@" & SkillListBox.Lines(iTemp)
            Next
            FinishDataBox.Text += "}"

            If SkillActionTypeBox.Text <> "" Then
                FinishDataBox.Text += vbTab & "skill_action_type=" & SkillActionTypeBox.Text
            Else
                MessageBox.Show("skill_action_type cant be empty. Select from list and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error)
                FinishDataBox.Text += vbNewLine
                Exit Sub
            End If
        End If

        'skill_prob=50 unit_tick=30 initial_delay = 1
        If SkillProbBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "skill_prob=" & SkillProbBox.Text
            If UnitTickBox.Text <> "" Then
                FinishDataBox.Text += vbTab & "unit_tick=" & UnitTickBox.Text
            Else
                MessageBox.Show("unit_tick cant be empty. Select from list and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error)
                FinishDataBox.Text += vbNewLine
                Exit Sub
            End If
        End If

        'initial_delay = 1 
        If InitialDelayBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "initial_delay=" & InitialDelayBox.Text
        Else
            If SkillNameBox.Text <> "" Or AreaTypeBox.Text = "damage" Or AreaTypeBox.Text = "poison" Then
                MessageBox.Show("initial_delay cant be empty. Select from list and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error)
                FinishDataBox.Text += vbNewLine
                Exit Sub
            End If
        End If

        ' Default_status
        If DefaultStatusBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "default_status=" & DefaultStatusBox.Text
        End If

        'on_time=120 off_time=5 random_time = 15
        'OnTimeBox OffTimeBox RandomTimeBox
        If OnTimeBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "on_time=" & OnTimeBox.Text
            If OffTimeBox.Text <> "" Then
                FinishDataBox.Text += vbTab & "off_time=" & OffTimeBox.Text
            Else
                MessageBox.Show("off_time cant be empty. Select from list and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error)
                FinishDataBox.Text += vbNewLine
                Exit Sub
            End If
            If RandomTimeBox.Text <> "" Then
                FinishDataBox.Text += vbTab & "random_time=" & RandomTimeBox.Text
            Else
                MessageBox.Show("random_time cant be empty. Select from list and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error)
                FinishDataBox.Text += vbNewLine
                Exit Sub
            End If
        End If

        'type=mother_tree affect_race=all	hp_regen_bonus=2	mp_regen_bonus=1	
        If AffectRaceBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "affect_race=" & AffectRaceBox.Text
        Else
            If AreaTypeBox.Text = "mother_tree" Then
                MessageBox.Show("affect_race cant be empty. Select from list and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error)
                FinishDataBox.Text += vbNewLine
                Exit Sub
            End If
        End If
        If HpRegenBonusBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "hp_regen_bonus=" & HpRegenBonusBox.Text
        End If
        If MpRegenBonusBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "mp_regen_bonus=" & MpRegenBonusBox.Text
        End If

        'DamageOnHpBox damage_on_hp
        If DamageOnHpBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "damage_on_hp=" & DamageOnHpBox.Text
        End If
        'DamageOnMpBox damage_on_mp
        If DamageOnMpBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "damage_on_mp=" & DamageOnMpBox.Text
        End If
        'MoveBonusBox move_bonus
        If MoveBonusBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "move_bonus=" & MoveBonusBox.Text
        End If

        'SSQ  exp_penalty_per=25 item_drop=off
        'DamageOnMpBox damage_on_mp
        If ExpPenaltyPerBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "exp_penalty_per=" & ExpPenaltyPerBox.Text
        End If
        'MoveBonusBox move_bonus
        If ItemDropBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "item_drop=" & ItemDropBox.Text
        End If

        'type=no_restart restart_time=1800	restart_allowed_time=30min'
        'RestartTimeBox
        If RestartTimeBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "restart_time=" & RestartTimeBox.Text
        Else
            If AreaTypeBox.Text = "no_restart" Then
                MessageBox.Show("restart_time cant be empty. Select from list and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error)
                FinishDataBox.Text += vbNewLine
                Exit Sub
            End If
        End If
        If RestartAllowedBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "restart_allowed_time=" & RestartAllowedBox.Text & "min"
        End If

        'type=peace_zone blocked_actions={priv_store;priv_rstore}
        'BlockedActionsListBox
        If BlockedActionsListBox.CheckedItems.Count > 0 Then
            FinishDataBox.Text += vbTab & "blocked_actions={"
            For iTemp = 0 To BlockedActionsListBox.CheckedItems.Count - 1
                FinishDataBox.Text += BlockedActionsListBox.CheckedItems.Item(iTemp).ToString
                If iTemp < BlockedActionsListBox.CheckedItems.Count - 1 Then
                    FinishDataBox.Text += ";"
                End If
            Next
            FinishDataBox.Text += "}"
        End If

        ' Messages
        'message_no
        If MessageNoBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "message_no=" & MessageNoBox.Text
        End If
        'event_id
        If EventIdBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "event_id=" & EventIdBox.Text
        End If
        'entering_message_no = 1054 leaving_message_no = 1055
        If EnterMesNoBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "entering_message_no=" & EnterMesNoBox.Text
        End If
        If LeaveMesNoBox.Text <> "" Then
            FinishDataBox.Text += vbTab & "leaving_message_no=" & LeaveMesNoBox.Text
        End If


        ' Range generator
        FinishDataBox.Text += vbTab & "range={"
        For iTemp = 0 To DataGrid.RowCount - 2
            FinishDataBox.Text += "{"
            For iTemp2 = 0 To DataGrid.ColumnCount - 1
                FinishDataBox.Text += DataGrid.Item(iTemp2, iTemp).Value.ToString
                If iTemp2 < (DataGrid.ColumnCount - 1) Then FinishDataBox.Text += ";"
            Next
            FinishDataBox.Text += "}"
            If iTemp < (DataGrid.RowCount - 2) Then FinishDataBox.Text += ";"
        Next
        FinishDataBox.Text += "}"

        FinishDataBox.Text += vbTab & "area_end" & vbNewLine

        TabControl.SelectTab("TabPage4")
    End Sub


    Private Sub QuitButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuitButton.Click
        Me.Close()
    End Sub

    Private Sub ButtonClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClear.Click
        FinishDataBox.Text = ""
    End Sub
End Class