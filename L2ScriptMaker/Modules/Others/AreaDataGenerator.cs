﻿using L2ScriptMaker.Extensions.VbCompatibleHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L2ScriptMaker.Modules.Others
{
	public partial class AreaDataGenerator : Form
	{
		public AreaDataGenerator()
		{
			InitializeComponent();
		}


		// area_begin name=[ssq_main_pvp_1721_01] map_no = {17;21} type=ssq_zone
		// exp_penalty_per=25 item_drop=off message_no = 686
		// default_status = off  event_id=2418008
		// damage_on_hp=200 damage_on_mp=0
		// range = {{-78713;110409;-4948;-4748};{-77923;110423;-4948;-4748};{-77923;110522;-4948;-4748};{-78708;110515;-4948;-4748}}
		// area_end

		private void StartButton_Click(object sender, EventArgs e)
		{
			int iTemp;

			// Check null value in areagrid
			int iTemp2;
			if (DataGrid.RowCount == 1)
			{
				MessageBox.Show("Zone coorninates empty. Enter and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error);
				FinishDataBox.Text = "";
				return;
			}

			var loopTo = DataGrid.RowCount - 2;
			for (iTemp = 0; iTemp <= loopTo; iTemp++)
			{
				var loopTo1 = DataGrid.ColumnCount - 1;
				for (iTemp2 = 0; iTemp2 <= loopTo1; iTemp2++)
				{
					if (DataGrid[iTemp2, iTemp].Value == null)
					{
						MessageBox.Show("Empty data in line: " + Conversions.ToString(iTemp + 1) + " column: " + Conversions.ToString(iTemp2 + 1) + "! Edit and calculate again.", "Wrong chance", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					if (!Int32.TryParse((string)DataGrid[iTemp2, iTemp].Value, out _))
					{
						MessageBox.Show("Value in line: " + Conversions.ToString(iTemp + 1) + " column: " + Conversions.ToString(iTemp2 + 1) + " is not Digital. Enter valid value and try again.", "Wrong value", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
				}
			}


			// ----------- Start Generation ------------------
			if (AutoClearBox.Checked == true)
				FinishDataBox.Text = "";
			FinishDataBox.Text += "// " + Conversions.ToString(DateAndTime.Now) + ", area zone generated by L2ScriptMaker: AreaData Generator" + Constants.vbNewLine;

			// Area Comment
			if (!string.IsNullOrEmpty(AreaCommentBox.Text))
				FinishDataBox.Text += "// " + AreaCommentBox.Text + Constants.vbNewLine;

			FinishDataBox.Text += "area_begin";
			// Area Name
			if (!string.IsNullOrEmpty(ZoneNameBox.Text))
				FinishDataBox.Text += Constants.vbTab + "name=[" + ZoneNameBox.Text.Replace(" ", "_").ToLower() + "]";
			else
			{
				MessageBox.Show("Zone name cant be empty. Enter and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error);
				FinishDataBox.Text += Constants.vbNewLine;
				return;
			}
			// Area Map_no
			FinishDataBox.Text += Constants.vbTab + "map_no={" + MapNo1Box.Text + ";" + MapNo2Box.Text + "}";
			// Area Type
			if (!string.IsNullOrEmpty(AreaTypeBox.Text))
				FinishDataBox.Text += Constants.vbTab + "type=" + AreaTypeBox.Text;
			else
			{
				MessageBox.Show("Zone Type cant be empty. Select from list and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error);
				FinishDataBox.Text += Constants.vbNewLine;
				return;
			}
			// target
			if (!string.IsNullOrEmpty(TypeTargetBox.Text))
				FinishDataBox.Text += Constants.vbTab + "target=" + TypeTargetBox.Text;

			// skill_name=[s_area_fire1] 
			if (!string.IsNullOrEmpty(SkillNameBox.Text))
				FinishDataBox.Text += Constants.vbTab + "skill_name=[" + SkillNameBox.Text + "]";

			// skill_list={@s_area_poison7;@s_area_pd_down3} skill_action_type=serial
			if (!string.IsNullOrEmpty(SkillListBox.Text))
			{
				FinishDataBox.Text += Constants.vbTab + "skill_list={";
				var loopTo2 = SkillListBox.Lines.Length - 1;
				for (iTemp = 0; iTemp <= loopTo2; iTemp++)
				{
					if (iTemp > 0 & iTemp < SkillListBox.Lines.Length)
						FinishDataBox.Text += ";";
					FinishDataBox.Text += "@" + SkillListBox.Lines[iTemp];
				}
				FinishDataBox.Text += "}";

				if (!string.IsNullOrEmpty(SkillActionTypeBox.Text))
					FinishDataBox.Text += Constants.vbTab + "skill_action_type=" + SkillActionTypeBox.Text;
				else
				{
					MessageBox.Show("skill_action_type cant be empty. Select from list and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error);
					FinishDataBox.Text += Constants.vbNewLine;
					return;
				}
			}

			// skill_prob=50 unit_tick=30 initial_delay = 1
			if (!string.IsNullOrEmpty(SkillProbBox.Text))
			{
				FinishDataBox.Text += Constants.vbTab + "skill_prob=" + SkillProbBox.Text;
				if (!string.IsNullOrEmpty(UnitTickBox.Text))
					FinishDataBox.Text += Constants.vbTab + "unit_tick=" + UnitTickBox.Text;
				else
				{
					MessageBox.Show("unit_tick cant be empty. Select from list and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error);
					FinishDataBox.Text += Constants.vbNewLine;
					return;
				}
			}

			// initial_delay = 1 
			if (!string.IsNullOrEmpty(InitialDelayBox.Text))
				FinishDataBox.Text += Constants.vbTab + "initial_delay=" + InitialDelayBox.Text;
			else if (!string.IsNullOrEmpty(SkillNameBox.Text) | (AreaTypeBox.Text ?? "") == "damage" | (AreaTypeBox.Text ?? "") == "poison")
			{
				MessageBox.Show("initial_delay cant be empty. Select from list and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error);
				FinishDataBox.Text += Constants.vbNewLine;
				return;
			}

			// Default_status
			if (!string.IsNullOrEmpty(DefaultStatusBox.Text))
				FinishDataBox.Text += Constants.vbTab + "default_status=" + DefaultStatusBox.Text;

			// on_time=120 off_time=5 random_time = 15
			// OnTimeBox OffTimeBox RandomTimeBox
			if (!string.IsNullOrEmpty(OnTimeBox.Text))
			{
				FinishDataBox.Text += Constants.vbTab + "on_time=" + OnTimeBox.Text;
				if (!string.IsNullOrEmpty(OffTimeBox.Text))
					FinishDataBox.Text += Constants.vbTab + "off_time=" + OffTimeBox.Text;
				else
				{
					MessageBox.Show("off_time cant be empty. Select from list and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error);
					FinishDataBox.Text += Constants.vbNewLine;
					return;
				}
				if (!string.IsNullOrEmpty(RandomTimeBox.Text))
					FinishDataBox.Text += Constants.vbTab + "random_time=" + RandomTimeBox.Text;
				else
				{
					MessageBox.Show("random_time cant be empty. Select from list and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error);
					FinishDataBox.Text += Constants.vbNewLine;
					return;
				}
			}

			// type=mother_tree affect_race=all	hp_regen_bonus=2	mp_regen_bonus=1	
			if (!string.IsNullOrEmpty(AffectRaceBox.Text))
				FinishDataBox.Text += Constants.vbTab + "affect_race=" + AffectRaceBox.Text;
			else if ((AreaTypeBox.Text ?? "") == "mother_tree")
			{
				MessageBox.Show("affect_race cant be empty. Select from list and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error);
				FinishDataBox.Text += Constants.vbNewLine;
				return;
			}
			if (!string.IsNullOrEmpty(HpRegenBonusBox.Text))
				FinishDataBox.Text += Constants.vbTab + "hp_regen_bonus=" + HpRegenBonusBox.Text;
			if (!string.IsNullOrEmpty(MpRegenBonusBox.Text))
				FinishDataBox.Text += Constants.vbTab + "mp_regen_bonus=" + MpRegenBonusBox.Text;

			// DamageOnHpBox damage_on_hp
			if (!string.IsNullOrEmpty(DamageOnHpBox.Text))
				FinishDataBox.Text += Constants.vbTab + "damage_on_hp=" + DamageOnHpBox.Text;
			// DamageOnMpBox damage_on_mp
			if (!string.IsNullOrEmpty(DamageOnMpBox.Text))
				FinishDataBox.Text += Constants.vbTab + "damage_on_mp=" + DamageOnMpBox.Text;
			// MoveBonusBox move_bonus
			if (!string.IsNullOrEmpty(MoveBonusBox.Text))
				FinishDataBox.Text += Constants.vbTab + "move_bonus=" + MoveBonusBox.Text;

			// SSQ  exp_penalty_per=25 item_drop=off
			// DamageOnMpBox damage_on_mp
			if (!string.IsNullOrEmpty(ExpPenaltyPerBox.Text))
				FinishDataBox.Text += Constants.vbTab + "exp_penalty_per=" + ExpPenaltyPerBox.Text;
			// MoveBonusBox move_bonus
			if (!string.IsNullOrEmpty(ItemDropBox.Text))
				FinishDataBox.Text += Constants.vbTab + "item_drop=" + ItemDropBox.Text;

			// type=no_restart restart_time=1800	restart_allowed_time=30min'
			// RestartTimeBox
			if (!string.IsNullOrEmpty(RestartTimeBox.Text))
				FinishDataBox.Text += Constants.vbTab + "restart_time=" + RestartTimeBox.Text;
			else if ((AreaTypeBox.Text ?? "") == "no_restart")
			{
				MessageBox.Show("restart_time cant be empty. Select from list and try again.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error);
				FinishDataBox.Text += Constants.vbNewLine;
				return;
			}
			if (!string.IsNullOrEmpty(RestartAllowedBox.Text))
				FinishDataBox.Text += Constants.vbTab + "restart_allowed_time=" + RestartAllowedBox.Text + "min";

			// type=peace_zone blocked_actions={priv_store;priv_rstore}
			// BlockedActionsListBox
			if (BlockedActionsListBox.CheckedItems.Count > 0)
			{
				FinishDataBox.Text += Constants.vbTab + "blocked_actions={";
				var loopTo3 = BlockedActionsListBox.CheckedItems.Count - 1;
				for (iTemp = 0; iTemp <= loopTo3; iTemp++)
				{
					FinishDataBox.Text += BlockedActionsListBox.CheckedItems[iTemp].ToString();
					if (iTemp < BlockedActionsListBox.CheckedItems.Count - 1)
						FinishDataBox.Text += ";";
				}
				FinishDataBox.Text += "}";
			}

			// Messages
			// message_no
			if (!string.IsNullOrEmpty(MessageNoBox.Text))
				FinishDataBox.Text += Constants.vbTab + "message_no=" + MessageNoBox.Text;
			// event_id
			if (!string.IsNullOrEmpty(EventIdBox.Text))
				FinishDataBox.Text += Constants.vbTab + "event_id=" + EventIdBox.Text;
			// entering_message_no = 1054 leaving_message_no = 1055
			if (!string.IsNullOrEmpty(EnterMesNoBox.Text))
				FinishDataBox.Text += Constants.vbTab + "entering_message_no=" + EnterMesNoBox.Text;
			if (!string.IsNullOrEmpty(LeaveMesNoBox.Text))
				FinishDataBox.Text += Constants.vbTab + "leaving_message_no=" + LeaveMesNoBox.Text;


			// Range generator
			FinishDataBox.Text += Constants.vbTab + "range={";
			var loopTo4 = DataGrid.RowCount - 2;
			for (iTemp = 0; iTemp <= loopTo4; iTemp++)
			{
				FinishDataBox.Text += "{";
				var loopTo5 = DataGrid.ColumnCount - 1;
				for (iTemp2 = 0; iTemp2 <= loopTo5; iTemp2++)
				{
					FinishDataBox.Text += DataGrid[iTemp2, iTemp].Value.ToString();
					if (iTemp2 < DataGrid.ColumnCount - 1)
						FinishDataBox.Text += ";";
				}
				FinishDataBox.Text += "}";
				if (iTemp < DataGrid.RowCount - 2)
					FinishDataBox.Text += ";";
			}
			FinishDataBox.Text += "}";

			FinishDataBox.Text += Constants.vbTab + "area_end" + Constants.vbNewLine;

			TabControl.SelectTab("TabPage4");
		}


		private void QuitButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ButtonClear_Click(object sender, EventArgs e)
		{
			FinishDataBox.Text = "";
		}
	}
}
