using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;

//include GLM library
using GlmNet;


using System.IO;
using System.Diagnostics;

namespace Graphics
{
    // class of the coordinates that make the primitives shape of the drawing `
    class point
    {
        public float x;
        public float y;

        public point(float x, float y)
        {
            this.x = x; this.y = y;
        }
    }
    class Renderer
    {
        Shader sh;
        
        uint triangleBufferID;
        uint xyzAxesBufferID;

        //3D Drawing
        mat4 ModelMatrix;
        mat4 ViewMatrix;
        mat4 ProjectionMatrix;
        
        int ShaderModelMatrixID;
        int ShaderViewMatrixID;
        int ShaderProjectionMatrixID;

        const float rotationSpeed = 1f;
        float rotationAngle = 0;

        public float translationX=0, 
                     translationY=0, 
                     translationZ=0;

        Stopwatch timer = Stopwatch.StartNew();

        vec3 triangleCenter;

        public void Initialize()
        {
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            sh = new Shader(projectPath + "\\Shaders\\SimpleVertexShader.vertexshader", projectPath + "\\Shaders\\SimpleFragmentShader.fragmentshader");

            Gl.glClearColor(0, 0, 0, 1);
            point C = new point(  -05.216806023209f, 08.630236091905f);
            point D = new point(  -05.63345296616f,  06.31925322829f);
            point E = new point(  -03.25399737376f,  06.16389717743f);
            point F = new point(  -00.60257399805f,  07.728671628676f);
            point G = new point(  -01.21109739575f,  05.07724825295f);
            point H = new point(  -04.34064629824f,  04.07753124244f);
            point I = new point(  -06.17536530817f,  04.09395361107f);
            point J = new point(  -03.514793115643f, 03.860201457545f);
            point K = new point(  -1.4f, 3.2f);
            point L = new point(  -04.036384599391f, 02.599688705154f);
            point M = new point(  -06.862513640351f, 0);
            point N = new point(  -05.657828749108f, -00.163603790861f);
            point Q = new point(  -02.548060774038f, -08.512350246454f);
            point R = new point(  -04.593223496381f, -08.456318391047f);
            point T = new point(  -00.475110565087f, -07.240732818911f);
            point u = new point(   00.662193434019f, 0);
            point W = new point(   03.807737055014f, -03.075986552868f);
            point Z = new point(  -00.693466680384f, -08.473914469928f);
           point A1 = new point(  -00.564008776466f, -07.74279177399f);
           point B1 = new point(  -01.748436305868f, -08.491497297019f);
            point C1 = new point(  03.626507848634f, -08.496002329513f);
            point D1 = new point(  03.683805047445f, -06.782416932801f);
            point E1 = new point(  05.547013029527f, -08.290233917275f);
            point I1 = new point(  07.290733950829f, -04.011367930309f);
            point J1 = new point(  07.307476112011f, -07.032760286929f);
            point F1 = new point(  06.143f, -06.825f);
            point G1 = new point(  05.023181499324f, -00.63532531104f);
            point H1 = new point(  06.670848059404f, -05.532487866542f);
            point O = new point(  -05.220937619814f,  03.427202483255f);
            point P = new point(  -04.72114147347f,   03.078051208293f);
            point S = new point(  -02.397875754703f,  02.972781222232f);
            point V = new point(  -04.22101092962f,  -04.021006585522f);
            point K1 = new point( -05.664872619714f, -01.82309956053f);
            point M1 = new point( -02.500853914425f, -04.902289860425f);
            point N1 = new point( -02.412552288942f,  01.85041944481f);
            point S1 = new point( -04.074108257544f,  03.318742852459f);


            float[] triangleVertices= { 
		          //triangle CDE
                C.x , C.y , 0,               0.004f, 0.122f ,0.294f,

                D.x,D.y , 0 ,                0.004f, 0.122f ,0.294f,

                E.x , E.y , 0 ,              0.004f, 0.122f ,0.294f,


                //triangle EFG
                E .x , E .y , 0 ,          0.004f, 0.122f ,0.294f,

                F.x , F.y , 0 ,             0.004f, 0.122f ,0.294f,

                G.x , G.y , 0 ,             0.004f, 0.122f ,0.294f,



                 //triangle DIH
                D .x , D .y , 0 ,        0.702f, 0.804f, 0.878f,

                I.x , I.y , 0 ,          0.702f, 0.804f, 0.878f,

                H.x , H.y , 0 ,          0.702f, 0.804f, 0.878f,

               
                //triangle GKJ
                G .x , G .y , 0,        0.702f, 0.804f, 0.878f,

                K.x , K.y , 0 ,        0.702f, 0.804f, 0.878f,

                J.x , J.y , 0 ,        0.702f, 0.804f, 0.878f,


                 //triangle HJS1
                H .x , H .y , 0 ,0.004f, 0.122f ,0.294f,
                S1.x , S1.y , 0 ,0.004f, 0.122f ,0.294f,
                J.x , J.y , 0 ,  0.004f, 0.122f ,0.294f,
                
             
                  //triangle K1RV
                 R.x , R .y , 0 ,    0.004f, 0.122f ,0.294f,

                K1.x , K1.y , 0 ,   0.004f, 0.122f ,0.294f,

                V.x , V.y , 0 ,     0.004f, 0.122f ,0.294f,


                 //triangle ZUW
                 Z.x , Z .y , 0 , 0.000f, 0.357f, 0.588f,

                W.x , W.y , 0 ,   0.000f, 0.357f, 0.588f,

                u.x , u.y , 0 ,   0.000f, 0.357f, 0.588f, 


                 //triangle g1i1h1
                 G1.x , G1 .y , 0 ,0.000f, 0.357f, 0.588f,
                I1.x , I1.y , 0 ,  0.000f, 0.357f, 0.588f,
                H1.x , H1.y , 0 ,  0.000f, 0.357f, 0.588f, 


                    //triangle ZC1W
                 Z.x , Z .y , 0 , 0.702f, 0.804f, 0.878f,

                W.x , W.y , 0 ,   0.702f, 0.804f, 0.878f,

                C1.x , C1.y , 0 , 0.702f, 0.804f, 0.878f,


       //             //triangle F1D1E1
                 F1.x , F1 .y , 0 , 0.392f, 0.592f ,0.694f,

                E1.x , E1.y , 0 ,   0.392f, 0.592f ,0.694f,

                D1.x , D1.y , 0 ,   0.392f, 0.592f ,0.694f,


                    //triangle C1D1E1
                 C1.x , C1 .y ,0, 0.004f, 0.122f ,0.294f,

                E1.x , E1.y , 0 , 0.004f, 0.122f ,0.294f,

                D1.x , D1.y , 0 , 0.004f, 0.122f ,0.294f,



                      //triangle OPNVM  1 
                 O.x , O .y , 0 ,    0.392f, 0.592f ,0.694f,

                P.x , P.y , 0 ,      0.392f, 0.592f ,0.694f,

                N.x , N.y , 0 ,      0.392f, 0.592f ,0.694f,

                 V.x , V.y , 0 ,     0.392f, 0.592f ,0.694f,

                M.x , M.y , 0 ,      0.392f, 0.592f ,0.694f,


                //PSLN2
        //        
                P.x , P.y , 0 ,  0.000f, 0.357f, 0.588f,
                 L.x , L.y , 0 , 0.000f, 0.357f, 0.588f,
                S.x , S.y , 0 ,  0.000f, 0.357f, 0.588f,
                N.x , N.y , 0 ,  0.000f, 0.357f, 0.588f, 

                   //triangle QNS
                Q .x , Q .y , 0 ,0.702f, 0.804f, 0.878f,
                S.x , S.y , 0 ,  0.702f, 0.804f, 0.878f,
                N.x , N.y , 0 ,  0.702f, 0.804f, 0.878f,

                 //KLS1J
                
                K.x , K.y , 0 ,  0.392f, 0.592f ,0.694f,

                 L.x , L.y , 0 , 0.392f, 0.592f ,0.694f,

                S1.x , S1.y ,0 , 0.392f, 0.592f ,0.694f,


                J.x , J.y , 0 , 0.392f, 0.592f ,0.694f,

                  //LS1HI
                
                
                 L.x , L.y , 0 ,  0.392f, 0.592f ,0.694f,

                S1.x , S1.y , 0 , 0.392f, 0.592f ,0.694f,

                H.x , H.y , 0 ,   0.392f, 0.592f ,0.694f,

                I.x , I.y , 0  ,  0.392f, 0.592f ,0.694f,


                       //TM1N1U
                
                
                 T.x , T.y , 0 ,  0.004f, 0.122f ,0.294f,

                M1.x , M1.y , 0 , 0.004f, 0.122f ,0.294f,

                N1.x , N1.y , 0 , 0.004f, 0.122f ,0.294f,

                u.x , u.y , 0 ,   0.004f, 0.122f ,0.294f,


                //i1h1f1e1j1
                 
                 I1.x , I1.y , 0 , 0.702f, 0.804f, 0.878f,

                H1.x , H1.y , 0 ,  0.702f, 0.804f, 0.878f,

                F1.x , F1.y , 0 ,  0.702f, 0.804f, 0.878f,

                E1.x , E1.y , 0 ,  0.702f, 0.804f, 0.878f,

                J1.x , J1.y , 0 ,  0.702f, 0.804f, 0.878f,


                //p1za1
                     B1 .x , B1 .y , 0 ,0.702f, 0.804f, 0.878f,
                Z.x , Z.y , 0 ,  0.702f, 0.804f, 0.878f,
                A1.x , A1.y , 0 ,  0.702f, 0.804f, 0.878f,

                //EDHJG
                E.x , E.y , 0 ,     0.392f, 0.592f ,0.694f,


                D .x , D .y , 0 ,   0.392f, 0.592f ,0.694f,


                J.x , J.y , 0 ,     0.392f, 0.592f ,0.694f,

                H.x , H.y , 0 ,     0.392f, 0.592f ,0.694f,
                               G.x , G.y , 0 ,      0.392f, 0.592f ,0.694f,


            }; // Triangle Center = (10, 7, -5)
            
            triangleCenter = new vec3(10, 7, -5);

            float[] xyzAxesVertices = {
		        //x
		        0.0f, 0.0f, 0.0f,
                1.0f, 0.0f, 0.0f, 
		        100.0f, 0.0f, 0.0f,
                1.0f, 0.0f, 0.0f, 
		        //y
	            0.0f, 0.0f, 0.0f,
                0.0f,1.0f, 0.0f, 
		        0.0f, 100.0f, 0.0f,
                0.0f, 1.0f, 0.0f, 
		        //z
	            0.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f,  
		        0.0f, 0.0f, -100.0f,
                0.0f, 0.0f, 1.0f,  
            };


            triangleBufferID = GPU.GenerateBuffer(triangleVertices);
            xyzAxesBufferID = GPU.GenerateBuffer(xyzAxesVertices);

            // View matrix 
            ViewMatrix = glm.lookAt(
                       new vec3(50, 50, 50), // Camera is at (0,5,5), in World Space
                        new vec3(0, 0, 0), // and looks at the origin
                        new vec3(0,1,0)  // Head is up (set to 0,-1,0 to look upside-down)
                );
            // Model Matrix Initialization
            ModelMatrix = new mat4(1);

            //ProjectionMatrix = glm.perspective(FOV, Width / Height, Near, Far);
            ProjectionMatrix = glm.perspective(45.0f, 4.0f / 3.0f, 0.1f, 100.0f);
            
            // Our MVP matrix which is a multiplication of our 3 matrices 
            sh.UseShader();
          

            //Get a handle for our "MVP" uniform (the holder we created in the vertex shader)
            ShaderModelMatrixID = Gl.glGetUniformLocation(sh.ID, "modelMatrix");
            ShaderViewMatrixID = Gl.glGetUniformLocation(sh.ID, "viewMatrix");
            ShaderProjectionMatrixID = Gl.glGetUniformLocation(sh.ID, "projectionMatrix");

            Gl.glUniformMatrix4fv(ShaderViewMatrixID, 1, Gl.GL_FALSE, ViewMatrix.to_array());
            Gl.glUniformMatrix4fv(ShaderProjectionMatrixID, 1, Gl.GL_FALSE, ProjectionMatrix.to_array());
         timer.Start();
        }
       
        public void Draw()
        {
            sh.UseShader();
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            #region XYZ axis

            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, xyzAxesBufferID);
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, new mat4(1).to_array()); // Identity

            Gl.glEnableVertexAttribArray(0);
            Gl.glEnableVertexAttribArray(1);

            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));
             
            Gl.glDrawArrays(Gl.GL_LINES, 0, 6);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);

            #endregion

            #region Animated Triangle
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, triangleBufferID);
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());

            Gl.glEnableVertexAttribArray(0);
            Gl.glEnableVertexAttribArray(1);

            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));

            //  Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 3, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 6, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 9, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 12, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 15, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 18, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 21, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 24, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 27, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 30, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLE_FAN, 33, 5);
            Gl.glDrawArrays(Gl.GL_TRIANGLE_FAN, 38, 4);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 42, 3);

            Gl.glDrawArrays(Gl.GL_TRIANGLE_FAN, 45, 4);
            Gl.glDrawArrays(Gl.GL_TRIANGLE_FAN, 49, 4);
            Gl.glDrawArrays(Gl.GL_TRIANGLE_FAN, 53, 4);
            Gl.glDrawArrays(Gl.GL_TRIANGLE_FAN, 57, 5);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 62, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLE_FAN, 65, 5);
            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            #endregion
        }
        

        public void Update()
        {

            timer.Stop();
           
            var deltaTime = timer.ElapsedMilliseconds/1000.0f;

            rotationAngle += deltaTime * rotationSpeed;

            List<mat4> transformations = new List<mat4>();
            transformations.Add(glm.translate(new mat4(1), -1 * triangleCenter));
            transformations.Add(glm.rotate(rotationAngle, new vec3(0, 1, 0)));
            transformations.Add(glm.translate(new mat4(1),  triangleCenter));
            transformations.Add(glm.translate(new mat4(1), new vec3(translationX, translationY, translationZ)));

            ModelMatrix =  MathHelper.MultiplyMatrices(transformations);
            
            timer.Reset();
            timer.Start();
        }
        
        public void CleanUp()
        {
            sh.DestroyShader();
        }
    }
}
