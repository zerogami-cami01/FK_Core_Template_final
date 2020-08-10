using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using FK_CLI;

namespace FK_Core_Template3_3
{
    class Program
    {
        static fk_AppWindow WindowSetup()
        {
            var window = new fk_AppWindow();
            window.Size = new fk_Dimension(800, 800);
            window.TrackBallMode = true;
            return window;
        }

        static fk_Model CameraSetup(fk_AppWindow argWindow)
        {
            var camera = new fk_Model();
            var cameraBlock = new fk_Block(10.0, 10.0, 10.0);

            camera.Shape = cameraBlock;
            argWindow.CameraModel = camera;
            //-- OBB--
            camera.BMode = fk_BoundaryMode.OBB;
            camera.AdjustSphere();
            camera.AdjustOBB();
            //camera.BDraw = true;
            //
            argWindow.Entry(camera);
            argWindow.CameraPos = new fk_Vector(0.0, 20.0, 200.0);
            argWindow.CameraFocus = new fk_Vector(0.0, 0.0, 0.0);

            return camera;
        }

        static void modelGroundSetup(fk_AppWindow argWindow, double argX, double argY, double argZ)
        {
            var block = new fk_Block(argX, argY, argZ);
            var blockModel = new fk_Model();
            // Material設定
            var mat = new fk_Material();
            mat.Ambient = mat.Diffuse = new fk_Color(1.0, 1.0, 1.0);
            mat.Alpha = 0.9f;
            blockModel.Material = mat;

            blockModel.Shape = block;

            argWindow.Entry(blockModel);
        }

        static fk_Model BlockSetup(fk_AppWindow argWindow, double argX, double argY, double argZ, double argTX, double argTY, double argTZ)
        {
            var block = new fk_Block(argX, argY, argZ);
            var blockModel = new fk_Model();

            blockModel.Shape = block;
            blockModel.LoTranslate(argTX, argTY, argTZ);
            // Material設定
            var mat = new fk_Material();
            mat.Ambient = mat.Diffuse = new fk_Color(1.0, 1.0, 0.0);
            mat.Alpha = 0.8f;
            blockModel.Material = mat;
            //-- OBB--
            blockModel.BMode = fk_BoundaryMode.OBB;
            blockModel.AdjustSphere();
            blockModel.AdjustOBB();
            blockModel.BDraw = true;
            //
            argWindow.Entry(blockModel);

            return blockModel;
        }

        static fk_Model WorldTreeSetup(fk_AppWindow argWindow, int argR, double argX, double argY, double argZ)
        {
            var prism = new fk_Prism(argR, argX, argY, argZ);
            var cone = new fk_Cone(argR, argX * 2, argZ * 2);
            var sphere = new fk_Sphere(4, 5.0);
            int n = 12;
            var blockModel1 = new fk_Model[n];

            for (int i = 0; i < n; i++)
            {
                blockModel1[i] = new fk_Model();
            }

            blockModel1[0].Shape = prism;
            blockModel1[1].Shape = cone;
            blockModel1[2].Shape = sphere;
            blockModel1[3].Shape = sphere;
            blockModel1[4].Shape = sphere;
            blockModel1[5].Shape = sphere;
            blockModel1[6].Shape = sphere;
            blockModel1[7].Shape = sphere;
            blockModel1[8].Shape = sphere;
            blockModel1[9].Shape = sphere;
            blockModel1[10].Shape = sphere;
            blockModel1[11].Shape = sphere;

            // Material設定
            var mat1 = new fk_Material();
            mat1.Ambient = mat1.Diffuse = new fk_Color(0.2, 0.1, 0);
            mat1.Alpha = 0.8f;
            var mat2 = new fk_Material();
            mat2.Ambient = mat2.Diffuse = new fk_Color(0.0, 0.3, 0.0);
            mat2.Alpha = 0.7f;
            blockModel1[0].Material = mat1;
            blockModel1[0].LoAngle(0.0, Math.PI / 2.0, 0.0);
            blockModel1[1].Material = mat2;
            blockModel1[1].GlTranslate(0.0, 0.0, -argZ);

            var mat3 = new fk_Material();
            mat3.Ambient = mat3.Diffuse = new fk_Color(1.0, 0.0, 0.0);
            mat3.Alpha = 0.6f;
            blockModel1[2].Material = mat3;
            blockModel1[2].GlTranslate(-20.0, -20.0, -140);
            blockModel1[3].Material = mat3;
            blockModel1[3].GlTranslate(20.0, 20.0, -140);
            var mat4 = new fk_Material();
            mat4.Ambient = mat4.Diffuse = new fk_Color(0.0, 0.0, 1.0);
            mat4.Alpha = 0.6f;
            blockModel1[4].Material = mat4;
            blockModel1[4].GlTranslate(-23.0, 23.0, -110);
            blockModel1[5].Material = mat4;
            blockModel1[5].GlTranslate(23.0, -23.0, -110);
            var mat5 = new fk_Material();
            mat5.Ambient = mat5.Diffuse = new fk_Color(1.0, 1.0, 0.0);
            mat5.Alpha = 0.6f;
            blockModel1[6].Material = mat5;
            blockModel1[6].GlTranslate(-12.0, 12.0, -170);
            blockModel1[7].Material = mat5;
            blockModel1[7].GlTranslate(12.0, -12.0, -170);
            var mat6 = new fk_Material();
            mat6.Ambient = mat6.Diffuse = new fk_Color(1.0, 0.0, 1.0);
            mat6.Alpha = 0.6f;
            blockModel1[8].Material = mat6;
            blockModel1[8].GlTranslate(28.0, 28.0, -90);
            blockModel1[9].Material = mat6;
            blockModel1[9].GlTranslate(-28.0, -28.0, -90);
            var mat7 = new fk_Material();
            mat7.Ambient = mat7.Diffuse = new fk_Color(0.0, 1.0, 1.0);
            mat7.Alpha = 0.6f;
            blockModel1[10].Material = mat7;
            blockModel1[10].GlTranslate(7.0, 7.0, -200);
            blockModel1[11].Material = mat7;
            blockModel1[11].GlTranslate(-7.0, -7.0, -200);

            var FormationDelta = blockModel1[0];

            for (int i = 0; i < n; i++)
            {
                //-- OBB--
                blockModel1[i].BMode = fk_BoundaryMode.OBB;
                blockModel1[i].AdjustSphere();
                blockModel1[i].AdjustOBB();
                //blockModel1[i].BDraw = true;
                //
                argWindow.Entry(blockModel1[i]);
                //親子処理
                blockModel1[i].Parent = FormationDelta;
            }

            return FormationDelta;
        }

        static fk_Model WallTreeSetup(fk_AppWindow argWindow, int argR, double argX, double argY, double argZ, double m, double l)
        {
            var prism = new fk_Prism(argR, argX, argY, argZ);
            var cone = new fk_Cone(argR, argX, argZ / 2);
            int n = 2;
            var blockModel2 = new fk_Model[n];

            for (int i = 0; i < n; i++)
            {
                blockModel2[i] = new fk_Model();
            }

            blockModel2[0].Shape = prism;
            blockModel2[1].Shape = cone;
            // Material設定
            var mat1 = new fk_Material();
            mat1.Ambient = mat1.Diffuse = new fk_Color(0.3, 0.1, 0);
            mat1.Alpha = 0.5f;
            var mat2 = new fk_Material();
            mat2.Ambient = mat2.Diffuse = new fk_Color(0.7, 0.5, 0.5);
            mat2.Alpha = 0.5f;
            blockModel2[0].Material = mat1;
            blockModel2[0].LoAngle(0.0, Math.PI / 2.0, 0.0);
            blockModel2[0].GlTranslate(350.0 + 10 * m, 0.0, 350.0 + 10 * l);
            blockModel2[1].Material = mat2;
            blockModel2[1].GlTranslate(0.0, 0.0, -argZ);

            var FormationDelta = blockModel2[0];

            for (int i = 0; i < n; i++)
            {
                //-- OBB--
                blockModel2[i].BMode = fk_BoundaryMode.OBB;
                blockModel2[i].AdjustSphere();
                blockModel2[i].AdjustOBB();
                //blockModel2[i].BDraw = true;
                //
                argWindow.Entry(blockModel2[i]);
                //親子処理
                blockModel2[i].Parent = FormationDelta;
            }

            return FormationDelta;
        }

        static fk_Model SnowManSetup(fk_AppWindow argWindow, int argN, double argR, double argX)
        {
            var sphere1 = new fk_Sphere(argN, argR);
            var sphere2 = new fk_Sphere(argN, argR / 4 * 3);
            var sphere3 = new fk_Sphere(4, 2.0);
            int n = 4;
            var blockModel2 = new fk_Model[n];

            for (int i = 0; i < n; i++)
            {
                blockModel2[i] = new fk_Model();
            }

            blockModel2[0].Shape = sphere1;
            blockModel2[1].Shape = sphere2;
            blockModel2[2].Shape = sphere3;
            blockModel2[3].Shape = sphere3;
            // Material設定
            var mat1 = new fk_Material();
            mat1.Ambient = mat1.Diffuse = new fk_Color(1.0, 1.0, 1.0);
            mat1.Alpha = 1.0f;
            var mat2 = new fk_Material();
            mat2.Ambient = mat2.Diffuse = new fk_Color(0.0, 0.0, 0.0);
            mat2.Alpha = 1.0f;
            blockModel2[0].Material = mat1;
            blockModel2[0].GlTranslate(argX, 20.0, 0.0);
            blockModel2[1].Material = mat1;
            blockModel2[1].GlTranslate(0.0, 25.0, 0.0);
            blockModel2[2].Material = mat2;
            blockModel2[2].GlTranslate(-5.0, 25.0, 14.0);
            blockModel2[3].Material = mat2;
            blockModel2[3].GlTranslate(5.0, 25.0, 14.0);

            var FormationDelta = blockModel2[0];

            for (int i = 0; i < n; i++)
            {
                //-- OBB--
                blockModel2[i].BMode = fk_BoundaryMode.OBB;
                blockModel2[i].AdjustSphere();
                blockModel2[i].AdjustOBB();
                //blockModel2[i].BDraw = true;
                //
                argWindow.Entry(blockModel2[i]);
                //親子処理
                blockModel2[i].Parent = FormationDelta;
            }

            return FormationDelta;
        }

        static (int, int) modelCheck(List<fk_Model> argMainModel, fk_Model argModelAll)
        {
            int Flag = 0;
            int nbr;
            int n = 0;
            var clone = new fk_Model();

            nbr = 0;
            while (nbr < argMainModel.Count)
            {
                if (argModelAll.IsInter(argMainModel[nbr]) == true)
                {
                    Flag = 1;
                    n = nbr;
                    clone = argMainModel[0];
                    argMainModel[0] = argMainModel[nbr];
                    argMainModel[nbr] = clone;
                    return (n, Flag);
                    //Console.WriteLine("if");
                }
                ++nbr;
            }
            //Console.WriteLine(n);
            return (n, Flag); // この関数は0しか返さない。何故かわからないけど。
        }

        static int allModelCheck(List<fk_Model> argMainModel)
        {
            int Flag = 0;
            int x, y;

            x = 0;
            while (x < argMainModel.Count)
            {
                y = x + 1;
                while (y < argMainModel.Count)
                {
                    if (argMainModel[x].IsInter(argMainModel[y]) == true)
                    {
                        Flag = 1;
                        Console.WriteLine("{0}, {1}", x, y);
                        return Flag;
                    }
                    y++;
                }
                x++;
            }
            return Flag;
        }

        static List<fk_Model> WallTreeList(fk_AppWindow argWindow, fk_Model argModelAll, int argCountWallTree0, int argCountWallTree1, int argCountWallTree2, int argCountWallTree3)
        {
            var WallTreeList = new List<fk_Model>();

            WallTreeList.Add(WallTreeSetup(argWindow, 80, 20.0, 20.0, 240.0, 0.0, 0.0));
            WallTreeList.Add(WallTreeSetup(argWindow, 80, 15.0, 15.0, 180.0, 0.0, -75.0));
            WallTreeList.Add(WallTreeSetup(argWindow, 80, 10.0, 10.0, 120.0, -75.0, 0.0));
            WallTreeList.Add(WallTreeSetup(argWindow, 80, 5.0, 5.0, 60.0, -75.0, -75.0));

            if (argModelAll.IsInter(WallTreeSetup(argWindow, 80, 20.0, 20.0, 240.0, 0.0, 0.0)) == true)
            {
                for (int i = 0; i < argCountWallTree0; i++)
                    WallTreeList.Add(WallTreeSetup(argWindow, 80, 20.0, 20.0, 240.0, 0.0, 0.0));
                //Console.WriteLine(argCountWallTree0);
            }
            else if (argModelAll.IsInter(WallTreeSetup(argWindow, 80, 15.0, 15.0, 180.0, 0.0, -75.0)) == true)
            {
                for (int i = 0; i < argCountWallTree1; i++)
                    WallTreeList.Add(WallTreeSetup(argWindow, 80, 15.0, 15.0, 180.0, 0.0, -75.0));
                //Console.WriteLine(argCountWallTree1);
            }
            else if (argModelAll.IsInter(WallTreeSetup(argWindow, 80, 10.0, 10.0, 120.0, -75.0, 0.0)) == true)
            {
                for (int i = 0; i < argCountWallTree2; i++)
                    WallTreeList.Add(WallTreeSetup(argWindow, 80, 5.0, 5.0, 60.0, -75.0, 0.0));
                //Console.WriteLine(argCountWallTree2);
            }
            else if (argModelAll.IsInter(WallTreeSetup(argWindow, 80, 5.0, 5.0, 60.0, -75.0, -75.0)) == true)
            {
                for (int i = 0; i < argCountWallTree3; i++)
                    WallTreeList.Add(WallTreeSetup(argWindow, 80, 5.0, 5.0, 60.0, -75.0, -75.0));
                //Console.WriteLine(argCountWallTree3);
            }

            return WallTreeList;
        }

        static void interferenceCheckMaterial(fk_Model argModelAll, double R, double G, double B)
        {
            var mat = new fk_Material();
            mat.Ambient = mat.Diffuse = new fk_Color(R, G, B);
            mat.Alpha = 0.8f;
            argModelAll.Material = mat;
        }

        static fk_ParticleSet ParticleSetup()
        {
            var particle = new fk_ParticleSet();
            particle.MaxSize = 2000; // パーティクル最大数
            particle.AllMode = true; // AllMethod() の有効化
            particle.IndivMode = true; // IndivMethod() の有効化

            // パーティクル生成時処理をラムダ式で設定
            particle.GenMethod = (P) =>
            {
                // 生成時の位置を(ランダムに)設定
                double x = fk_Math.DRand(-1000.0, 1000.0);
                double z = fk_Math.DRand(-1000.0, 1000.0);
                P.Position = new fk_Vector(x, 500.0, z);
            };

            // パーティクル全体処理をラムダ式で設定
            particle.AllMethod = () =>
            {
                for (int i = 0; i < 0.5; i++)
                {
                    if (fk_Math.DRand() < 0.3)
                    {
                        // 発生確率は30%（を5回）
                        particle.NewParticle();
                    }
                }
            };

            // パーティクル個別処理をタムダシキで設定
            particle.IndivMethod = (P) =>
            {
                fk_Vector pos;
                var water = new fk_Vector(0.0, -0.5, 0.0);

                pos = P.Position; // パーティクル位置取得
                pos.x = 0.0;
                P.Velocity = water;

                var col = new fk_Color();
                col.Set(0.8, 0.8, 0.8);
                P.Color = col;

                // パーティクルのx成分が-50以下になったら消去
                if (pos.y < -0.0)
                {
                    particle.RemoveParticle(P);
                }
            };

            // Main() にインスタンスを返す
            return particle;
        }

        static void ParticleModelSetup(fk_ParticleSet argParticule, fk_AppWindow argWindow)
        {
            var model = new fk_Model();
            model.Shape = argParticule.Shape;
            model.DrawMode = fk_Draw.POINT;

            //パーティクルの色
            model.ElementMode = fk_ElementMode.ELEMENT;

            // パーティクル描画の際の大きさ（ピクセル）
            model.PointSize = 5.0;

            argWindow.Entry(model);
        }

        static void Main(string[] args)
        {
            var window = WindowSetup();
            // 表示設定
            modelGroundSetup(window, 1500.0, 0.0, 1500.0);
            var WorldTree = WorldTreeSetup(window, 80, 20.0, 20.0, 80.0);
            var SnowMan1 = SnowManSetup(window, 10, 20.0, 100.0);
            var SnowMan2 = SnowManSetup(window, 10, 20.0, -100.0);

            // カメラ設定
            var camera = CameraSetup(window);
            var modelAll = BlockSetup(window, 10.0, 10.0, 10.0, 0.0, 5.0, 450.0);

            // 親子
            camera.Parent = modelAll;

            // モデルリスト作成
            var mainModel = new List<fk_Model>();
            mainModel.Add(WorldTree);
            mainModel.Add(SnowMan1);
            mainModel.Add(SnowMan2);
            for (int i = 1; i * 5 < 75; i++)
                mainModel.Add(WallTreeSetup(window, 80, 10.0, 10.0, 60.0, 0.0, -5.0 * i));
            for (int i = 1; i * 3 < 75; i++)
                mainModel.Add(WallTreeSetup(window, 80, 5.0, 5.0, 120.0, -75.0, -3.0 * i));
            for (int i = 1; i * 10 < 75; i++)
                mainModel.Add(WallTreeSetup(window, 80, 15.0, 15.0, 120.0, -10.0 * i, -75.0));

            int countWallTree0 = 0;
            int countWallTree1 = 0;
            int countWallTree2 = 0;
            int countWallTree3 = 0;

            for (int i = 0; i < WallTreeList(window, modelAll, countWallTree0, countWallTree1, countWallTree2, countWallTree3).Count; i++)
                mainModel.Add(WallTreeList(window, modelAll, countWallTree0, countWallTree1, countWallTree2, countWallTree3)[i]);
            //Console.WriteLine(mainModel.Count);

            fk_Material.InitDefault();
            // パーティクルセットの生成
            var particle = ParticleSetup();
            // パーティクルモデル設定
            ParticleModelSetup(particle, window);

            // window設定
            window.Open();

            int flag = 0;
            int param = modelCheck(mainModel, modelAll).Item1;
            Console.WriteLine(param);

            while (window.Update() == true)
            {
                // key操作
                if (window.GetSpecialKeyStatus(fk_Key.UP, fk_Switch.PRESS) == true)
                {
                    if (allModelCheck(mainModel) == 1 && flag == 1)
                        modelAll.LoTranslate(0.0, 0.0, 0.0);
                    else
                        modelAll.LoTranslate(0.0, 0.0, -5.0);
                }
                else if (window.GetSpecialKeyStatus(fk_Key.DOWN, fk_Switch.PRESS) == true)
                {
                    modelAll.LoTranslate(0.0, 0.0, 5.0);
                }
                else if (window.GetSpecialKeyStatus(fk_Key.RIGHT, fk_Switch.PRESS) == true)
                {
                    modelAll.GlRotateWithVec(modelAll.Position, fk_Axis.Y, -Math.PI / 100);
                }
                else if (window.GetSpecialKeyStatus(fk_Key.LEFT, fk_Switch.PRESS) == true)
                {
                    modelAll.GlRotateWithVec(modelAll.Position, fk_Axis.Y, Math.PI / 100);
                }
                //物体操作
                if (window.GetSpecialKeyStatus(fk_Key.CTRL_L, fk_Switch.PRESS) == true)
                {
                    // Material設定
                    interferenceCheckMaterial(modelAll, 1.0, 0.0, 1.0);
                    //
                    if (modelCheck(mainModel, modelAll).Item2 == 1 && flag == 0)
                    {
                        // 座標の初期化
                        mainModel[param].GlTranslate(-modelAll.Position);
                        // 親子設定
                        mainModel[param].Parent = modelAll;
                        Console.WriteLine(param);
                        flag = 1;

                        if (modelAll.IsInter(WallTreeList(window, modelAll, countWallTree0, countWallTree1, countWallTree2, countWallTree3)[0]) == true)
                        {
                            countWallTree0++;
                            mainModel.Add(WallTreeList(window, modelAll, countWallTree0, countWallTree1, countWallTree2, countWallTree3)
                                [WallTreeList(window, modelAll, countWallTree0, countWallTree1, countWallTree2, countWallTree3).Count - 1]);
                            //Console.WriteLine(countWallTree0);
                        }
                        else if (modelAll.IsInter(WallTreeList(window, modelAll, countWallTree0, countWallTree1, countWallTree2, countWallTree3)[1]) == true)
                        {
                            countWallTree1++;
                            mainModel.Add(WallTreeList(window, modelAll, countWallTree0, countWallTree1, countWallTree2, countWallTree3)
                                [WallTreeList(window, modelAll, countWallTree0, countWallTree1, countWallTree2, countWallTree3).Count - 1]);
                            //Console.WriteLine(countWallTree1);
                        }
                        else if (modelAll.IsInter(WallTreeList(window, modelAll, countWallTree0, countWallTree1, countWallTree2, countWallTree3)[2]) == true)
                        {
                            countWallTree2++;
                            mainModel.Add(WallTreeList(window, modelAll, countWallTree0, countWallTree1, countWallTree2, countWallTree3)
                                [WallTreeList(window, modelAll, countWallTree0, countWallTree1, countWallTree2, countWallTree3).Count - 1]);
                            //Console.WriteLine(countWallTree2);
                        }
                        else if (modelAll.IsInter(WallTreeList(window, modelAll, countWallTree0, countWallTree1, countWallTree2, countWallTree3)[3]) == true)
                        {
                            countWallTree3++;
                            mainModel.Add(WallTreeList(window, modelAll, countWallTree0, countWallTree1, countWallTree2, countWallTree3)
                                [WallTreeList(window, modelAll, countWallTree0, countWallTree1, countWallTree2, countWallTree3).Count - 1]);
                            //Console.WriteLine(countWallTree3);
                        }
                    }
                }
                else if (window.GetSpecialKeyStatus(fk_Key.CTRL_R, fk_Switch.PRESS) == true)
                {
                    // Material設定
                    interferenceCheckMaterial(modelAll, 0.0, 1.0, 1.0);
                    //
                    if (modelCheck(mainModel, modelAll).Item2 == 1 && flag == 1)
                    {
                        // 座標の移動
                        mainModel[param].GlTranslate(modelAll.Position);
                        // 親子解除
                        mainModel[param].DeleteParent(false);

                        //Console.WriteLine(param);
                        flag = 0;
                    }
                }
                else if (modelCheck(mainModel, modelAll).Item2 == 1)
                {
                    interferenceCheckMaterial(modelAll, 0.0, 0.0, 1.0);
                }
                else
                {
                    // Material設定
                    interferenceCheckMaterial(modelAll, 1.0, 1.0, 0.0);
                }

                SnowMan1.GlRotateWithVec(SnowMan1.Position, fk_Axis.Y, -Math.PI / 500.0);
                SnowMan2.GlRotateWithVec(SnowMan2.Position, fk_Axis.Y, Math.PI / 500.0);
                particle.Handle(); // パーティクルを１ステップ実行する
            }
        }
    }
}

