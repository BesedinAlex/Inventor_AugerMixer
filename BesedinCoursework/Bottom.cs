﻿using System;
using System.Windows.Forms;
using Inventor;

namespace BesedinCoursework
{
    public partial class Bottom : Form
    {
        InventorAPI api;
        private Inventor.Application app = null;
        double D1 = 440, H1 = 30, D2 = 350, H2 = 50, DR = 395, oR = 11; // Основание
        double L1 = 480, L2 = 440, T = 15, HB = 250, D3 = 250, H3 = 90; // "Коробка"
        double H4 = 400, D4D = 30, D4d = 20, A = 40; // Трубки
        double H5 = 205, D51 = 200, L51 = 20, D52 = 245, L52 = 45, D53 = 100, L53 = 22.5, D54 = 122.5, L54 = 35; // Крышка
        public Bottom()
        {
            InitializeComponent();
            textBox1.Text = Convert.ToString(H1 + H2 + HB + H3);
            textBox2.Text = Convert.ToString(D1);
            textBox3.Text = Convert.ToString(H1);
            textBox4.Text = Convert.ToString(DR);
            textBox5.Text = Convert.ToString(oR);
            textBox6.Text = Convert.ToString(D2);
            textBox7.Text = Convert.ToString(H2);
            textBox8.Text = Convert.ToString(L1);
            textBox9.Text = Convert.ToString(L2);
            textBox10.Text = Convert.ToString(HB);
            textBox11.Text = Convert.ToString(T);
            textBox12.Text = Convert.ToString(D3);
            textBox13.Text = Convert.ToString(H3);
            textBox14.Text = Convert.ToString(H4);
            textBox15.Text = Convert.ToString(D4D);
            textBox16.Text = Convert.ToString(D4d);
            textBox17.Text = Convert.ToString(A);
            textBox18.Text = Convert.ToString(H5);
            textBox19.Text = Convert.ToString(D51);
            textBox20.Text = Convert.ToString(L51);
            textBox21.Text = Convert.ToString(D52);
            textBox22.Text = Convert.ToString(L52);
            textBox23.Text = Convert.ToString(D53);
            textBox24.Text = Convert.ToString(L53);
            textBox25.Text = Convert.ToString(D54);
            textBox26.Text = Convert.ToString(L54);
        }
        private void Build_Click(object sender, EventArgs e)
        {
            try
            {
                app = (Inventor.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application");
            }
            catch
            {
                InventorControl IC = new InventorControl();
                IC.ShowDialog();
                return;
            }
            api = new InventorAPI(app, "КВ", "Корпус для выгрузки материалов");
            Parameters oParameters = api.getCompDef("КВ").Parameters;
            PlanarSketch[] sketch = new PlanarSketch[11];
            Profile[] profile = new Profile[11];
            SketchPoint[] point = new SketchPoint[10];
            SketchLine[] line = new SketchLine[10];
            SketchCircle[] circle = new SketchCircle[2];
            // Основание
            sketch[0] = api.sketch(api.getCompDef("КВ").WorkPlanes[2]);
            circle[0] = api.circle(sketch[0], api.point(sketch[0], 0, 0), D1 / 10 / 2);
            profile[0] = api.profile(sketch[0]);
            ExtrudeFeature oExtrudeDef = api.getCompDef("КВ").Features.ExtrudeFeatures.AddByDistanceExtent(profile[0], H1 / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kJoinOperation, profile[0]);
            sketch[1] = api.sketch(oExtrudeDef.Faces[3]);
            circle[0] = api.circle(sketch[1], api.point(sketch[1], 0, 0), D2 / 10 / 2);
            profile[1] = api.profile(sketch[1]);
            ExtrudeFeature oExtrudeDef1 = api.getCompDef("КВ").Features.ExtrudeFeatures.AddByDistanceExtent(profile[1], H2 / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kJoinOperation, profile[1]);
            // Кубическая часть корпуса
            sketch[2] = api.sketch(oExtrudeDef1.Faces[2]);
            point[0] = api.point(sketch[2], L2 / 10 / 2, L1 / 10 / 2);
            point[1] = api.point(sketch[2], point[0].Geometry.X, -point[0].Geometry.Y);
            point[2] = api.point(sketch[2], -point[0].Geometry.X, -point[0].Geometry.Y);
            point[3] = api.point(sketch[2], -point[0].Geometry.X, point[0].Geometry.Y);
            line[0] = api.line(sketch[2], point[0], point[1]);
            line[1] = api.line(sketch[2], point[1], point[2]);
            line[2] = api.line(sketch[2], point[2], point[3]);
            line[3] = api.line(sketch[2], point[3], point[0]);
            profile[2] = api.profile(sketch[2]);
            ExtrudeFeature oExtrudeDef2 = api.getCompDef("КВ").Features.ExtrudeFeatures.AddByDistanceExtent(profile[2], T / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kJoinOperation, profile[2]);
            PlanarSketch oSketch3 = api.getCompDef("КВ").Sketches.Add(oExtrudeDef2.Faces[6], false);
            point[0] = oSketch3.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(L2 / 10 / 2, L1 / 10 / 2), false);
            point[1] = oSketch3.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(point[0].Geometry.X, -point[0].Geometry.Y), false);
            point[2] = oSketch3.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(-point[0].Geometry.X, -point[0].Geometry.Y), false);
            point[3] = oSketch3.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(-point[0].Geometry.X, point[0].Geometry.Y), false);
            line[0] = oSketch3.SketchLines.AddByTwoPoints(point[0], point[1]);
            line[1] = oSketch3.SketchLines.AddByTwoPoints(point[1], point[2]);
            line[2] = oSketch3.SketchLines.AddByTwoPoints(point[2], point[3]);
            line[3] = oSketch3.SketchLines.AddByTwoPoints(point[3], point[0]);
            point[4] = oSketch3.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(point[0].Geometry.X - T / 10, point[0].Geometry.Y - T / 10), false);
            point[5] = oSketch3.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(point[0].Geometry.X - T / 10, -point[0].Geometry.Y + T / 10), false);
            point[6] = oSketch3.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(-point[0].Geometry.X + T / 10, -point[0].Geometry.Y + T / 10), false);
            point[7] = oSketch3.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(-point[0].Geometry.X + T / 10, point[0].Geometry.Y - T / 10), false);
            line[4] = oSketch3.SketchLines.AddByTwoPoints(point[4], point[5]);
            line[5] = oSketch3.SketchLines.AddByTwoPoints(point[5], point[6]);
            line[6] = oSketch3.SketchLines.AddByTwoPoints(point[6], point[7]);
            line[7] = oSketch3.SketchLines.AddByTwoPoints(point[7], point[4]);
            Profile oProfile3 = (Profile)oSketch3.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef3 = api.getCompDef("КВ").Features.ExtrudeFeatures.AddByDistanceExtent(oProfile3, (HB - T * 2) / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kJoinOperation, oProfile3);
            WorkPlane oWorkPlane4 = api.getCompDef("КВ").WorkPlanes.AddByPlaneAndOffset(api.getCompDef("КВ").WorkPlanes[2], (H1 + H2 + HB - T) / 10, false);
            oWorkPlane4.Visible = false;
            PlanarSketch oSketch4 = api.getCompDef("КВ").Sketches.Add(oWorkPlane4);
            point[0] = oSketch4.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(L2 / 10 / 2, L1 / 10 / 2), false);
            point[1] = oSketch4.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(point[0].Geometry.X, -point[0].Geometry.Y), false);
            point[2] = oSketch4.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(-point[0].Geometry.X, -point[0].Geometry.Y), false);
            point[3] = oSketch4.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(-point[0].Geometry.X, point[0].Geometry.Y), false);
            line[0] = oSketch4.SketchLines.AddByTwoPoints(point[0], point[1]);
            line[1] = oSketch4.SketchLines.AddByTwoPoints(point[1], point[2]);
            line[2] = oSketch4.SketchLines.AddByTwoPoints(point[2], point[3]);
            line[3] = oSketch4.SketchLines.AddByTwoPoints(point[3], point[0]);
            circle[0] = oSketch4.SketchCircles.AddByCenterRadius(api.getTransGeom("КВ").CreatePoint2d(0, 0), D3 / 2 / 10);
            Profile oProfile4 = (Profile)oSketch4.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef4 = api.getCompDef("КВ").Features.ExtrudeFeatures.AddByDistanceExtent(oProfile4, T / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kJoinOperation, oProfile4);
            // Переход к коническому корпусу
            PlanarSketch oSketch5 = api.getCompDef("КВ").Sketches.Add(oExtrudeDef4.Faces[3], false);
            circle[0] = oSketch5.SketchCircles.AddByCenterRadius(api.getTransGeom("КВ").CreatePoint2d(0, 0), D3 / 2 / 10);
            circle[1] = oSketch5.SketchCircles.AddByCenterRadius(api.getTransGeom("КВ").CreatePoint2d(0, 0), (D3 / 2 + T) / 10);
            Profile oProfile5 = (Profile)oSketch5.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef5 = api.getCompDef("КВ").Features.ExtrudeFeatures.AddByDistanceExtent(oProfile5, H3 / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kJoinOperation, oProfile5);
            // Отверстия у основания
            PlanarSketch oSketch6 = api.getCompDef("КВ").Sketches.Add(api.getCompDef("КВ").WorkPlanes[2]);
            point[0] = oSketch6.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(0, 0), false);
            point[1] = oSketch6.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(0, DR / 2 / 10), false);
            point[2] = oSketch6.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(DR / 2 / 10, 0), false);
            line[0] = oSketch6.SketchLines.AddByTwoPoints(point[0], point[1]);
            line[1] = oSketch6.SketchLines.AddByTwoPoints(point[0], point[2]);
            oSketch6.DimensionConstraints.AddTwoLineAngle(line[0], line[1], api.getTransGeom("КВ").CreatePoint2d(1, 1));
            oSketch6.GeometricConstraints.AddHorizontal((SketchEntity)line[1]);
            circle[0] = oSketch6.SketchCircles.AddByCenterRadius(api.getTransGeom("КВ").CreatePoint2d(DR / 10, -1), oR / 10);
            oParameters["d13"].Expression = "105 degree";
            oSketch6.GeometricConstraints.AddCoincident((SketchEntity)circle[0].CenterSketchPoint, (SketchEntity)point[1]);
            Profile oProfile6 = (Profile)oSketch6.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef6 = api.getCompDef("КВ").Features.ExtrudeFeatures.AddByDistanceExtent(oProfile6, H1 / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kCutOperation, oProfile6);
            ObjectCollection objCollection6 = app.TransientObjects.CreateObjectCollection();
            objCollection6.Add(oExtrudeDef6);
            CircularPatternFeature CircularPatternFeature6 = api.getCompDef("КВ").Features.CircularPatternFeatures.Add(objCollection6, api.getCompDef("КВ").WorkAxes[2], true, 12, "360 degree", true, PatternComputeTypeEnum.kIdenticalCompute);
            // Трубки
            PlanarSketch oSketch7 = api.getCompDef("КВ").Sketches.Add(api.getCompDef("КВ").WorkPlanes[1]);
            circle[0] = oSketch7.SketchCircles.AddByCenterRadius(api.getTransGeom("КВ").CreatePoint2d(H4 / 10, 0), D4D / 2 / 10);
            Profile oProfile7 = (Profile)oSketch7.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef7 = api.getCompDef("КВ").Features.ExtrudeFeatures.AddByDistanceExtent(oProfile7, D3 / 10 + T * 2 / 10 + A * 2 / 10, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection, PartFeatureOperationEnum.kJoinOperation, oProfile7);
            PlanarSketch oSketch8 = api.getCompDef("КВ").Sketches.Add(api.getCompDef("КВ").WorkPlanes[1]);
            circle[0] = oSketch8.SketchCircles.AddByCenterRadius(api.getTransGeom("КВ").CreatePoint2d(H4 / 10, 0), D4d / 2 / 10);
            Profile oProfile8 = (Profile)oSketch8.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef8 = api.getCompDef("КВ").Features.ExtrudeFeatures.AddByDistanceExtent(oProfile8, D3 / 10 + T * 2 / 10 + A * 2 / 10, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection, PartFeatureOperationEnum.kCutOperation, oProfile8);
            PlanarSketch oSketch9 = api.getCompDef("КВ").Sketches.Add(oExtrudeDef4.Faces[3], false);
            circle[0] = oSketch9.SketchCircles.AddByCenterRadius(api.getTransGeom("КВ").CreatePoint2d(0, 0), D3 / 2 / 10);
            Profile oProfile9 = (Profile)oSketch9.Profiles.AddForSolid();
            ExtrudeFeature oExtrudeDef9 = api.getCompDef("КВ").Features.ExtrudeFeatures.AddByDistanceExtent(oProfile9, H3 / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kCutOperation, oProfile9);
            // Крышка
            PlanarSketch oSketch10 = api.getCompDef("КВ").Sketches.Add(api.getCompDef("КВ").WorkPlanes[3]);
            point[0] = oSketch10.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(L2 / 2 / 10, H5 / 10), false);
            point[1] = oSketch10.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(point[0].Geometry.X, point[0].Geometry.Y + D51 / 2 / 10), false);
            point[2] = oSketch10.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(point[1].Geometry.X + L51 / 10, point[1].Geometry.Y), false);
            point[3] = oSketch10.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(point[2].Geometry.X, point[0].Geometry.Y + D52 / 2 / 10), false);
            point[4] = oSketch10.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(point[3].Geometry.X + L52 / 10, point[3].Geometry.Y), false);
            point[5] = oSketch10.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(point[4].Geometry.X, point[0].Geometry.Y + D53 / 2 / 10), false);
            point[6] = oSketch10.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(point[5].Geometry.X + L53 / 10, point[5].Geometry.Y), false);
            point[7] = oSketch10.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(point[6].Geometry.X, point[0].Geometry.Y + D54 / 2 / 10), false);
            point[8] = oSketch10.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(point[7].Geometry.X + L54 / 10, point[7].Geometry.Y), false);
            point[9] = oSketch10.SketchPoints.Add(api.getTransGeom("КВ").CreatePoint2d(point[8].Geometry.X, point[0].Geometry.Y), false);
            line[0] = oSketch10.SketchLines.AddByTwoPoints(point[0], point[1]);
            line[1] = oSketch10.SketchLines.AddByTwoPoints(point[1], point[2]);
            line[2] = oSketch10.SketchLines.AddByTwoPoints(point[2], point[3]);
            line[3] = oSketch10.SketchLines.AddByTwoPoints(point[3], point[4]);
            line[4] = oSketch10.SketchLines.AddByTwoPoints(point[4], point[5]);
            line[5] = oSketch10.SketchLines.AddByTwoPoints(point[5], point[6]);
            line[6] = oSketch10.SketchLines.AddByTwoPoints(point[6], point[7]);
            line[7] = oSketch10.SketchLines.AddByTwoPoints(point[7], point[8]);
            line[8] = oSketch10.SketchLines.AddByTwoPoints(point[8], point[9]);
            line[9] = oSketch10.SketchLines.AddByTwoPoints(point[9], point[0]);
            Profile oProfile10 = (Profile)oSketch10.Profiles.AddForSolid();
            RevolveFeature revolvefeature10 = api.getCompDef("КВ").Features.RevolveFeatures.AddFull(oProfile10, line[9], PartFeatureOperationEnum.kJoinOperation);
            MessageBox.Show("Создание корпуса для выгрузки материалов завершено.", Text);
        }
        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                app = (Inventor.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application");
            }
            catch
            {
                InventorControl IC = new InventorControl();
                IC.ShowDialog();
                return;
            }
            try
            {
                saveFileDialog1.Filter = "Inventor Part Document|*.ipt";
                saveFileDialog1.Title = "Сохранение корпуса для выгрузки материалов";
                saveFileDialog1.FileName = api.getPartDoc("КВ").DisplayName;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    if (!string.IsNullOrWhiteSpace(saveFileDialog1.FileName))
                    {
                        api.getPartDoc("КВ").SaveAs(saveFileDialog1.FileName, false);
                        api.setFileName("КВ", saveFileDialog1.FileName);
                    }
                Close();
            }
            catch
            {
                MessageBox.Show("Деталь ещё не создана, либо программа не видит созданную деталь.", Text);
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e) =>
            D1 = Auto.checkTextBoxChange(textBox2, 440);
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            H1 = Auto.checkTextBoxChange(textBox3, 30);
            textBox1.Text = Convert.ToString(H1 + H2 + HB + H3);
        }
        private void textBox4_TextChanged(object sender, EventArgs e) =>
            DR = Auto.checkTextBoxChange(textBox4, 395);
        private void textBox5_TextChanged(object sender, EventArgs e) =>
            oR = Auto.checkTextBoxChange(textBox5, 11);
        private void textBox6_TextChanged(object sender, EventArgs e) =>
            D2 = Auto.checkTextBoxChange(textBox6, 350);
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            H2 = Auto.checkTextBoxChange(textBox7, 50);
            textBox1.Text = Convert.ToString(H1 + H2 + HB + H3);
        }
        private void textBox8_TextChanged(object sender, EventArgs e) =>
            L1 = Auto.checkTextBoxChange(textBox8, 480);
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox9, 440);
            L2 = Convert.ToDouble(textBox9.Text);
        }
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox10, 250);
            HB = Convert.ToDouble(textBox10.Text);
            textBox1.Text = Convert.ToString(H1 + H2 + HB + H3);
        }
        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox11, 15);
            T = Convert.ToDouble(textBox11.Text);
        }
        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox12, 250);
            D3 = Convert.ToDouble(textBox12.Text);
        }
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox13, 90);
            H3 = Convert.ToDouble(textBox13.Text);
            textBox1.Text = Convert.ToString(H1 + H2 + HB + H3);
        }
        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox14, 400);
            H4 = Convert.ToDouble(textBox14.Text);
        }
        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox15, 30);
            D4D = Convert.ToDouble(textBox15.Text);
        }
        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox16, 20);
            D4d = Convert.ToDouble(textBox16.Text);
        }
        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox17, 40);
            A = Convert.ToDouble(textBox17.Text);
        }
        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox18, 205);
            H5 = Convert.ToDouble(textBox18.Text);
        }
        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox19, 200);
            D51 = Convert.ToDouble(textBox19.Text);
        }
        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox20, 20);
            L51 = Convert.ToDouble(textBox20.Text);
        }
        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox21, 245);
            D52 = Convert.ToDouble(textBox21.Text);
        }
        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox22, 45);
            L52 = Convert.ToDouble(textBox22.Text);
        }
        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox23, 100);
            D53 = Convert.ToDouble(textBox23.Text);
        }
        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox24, 22.5);
            L53 = Convert.ToDouble(textBox24.Text);
        }
        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox25, 122.5);
            D54 = Convert.ToDouble(textBox25.Text);
        }
        private void textBox26_TextChanged(object sender, EventArgs e)
        {
            error_check(textBox26, 35);
            L54 = Convert.ToDouble(textBox26.Text);
        }
    }
}