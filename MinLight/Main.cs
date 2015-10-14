// MiniLight C# port
// Chris Lomont 2009
// www.lomont.org
using System;
using System.IO;

// todo - clean all casts in all files
// todo - change loops into counting up when possible
// todo - 80 character lines
// todo - multithread option
// todo - remove all conditional ? - replace with if else

namespace MinLight
	{
	    using MinLight.Entities;

	    class Program
		{

		static string MODEL_FORMAT_ID = "#MiniLight";
		static double SAVE_PERIOD = 180.0; // seconds

		/// <summary>
		/// Program start. Takes one command line parameter to get a filename
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		static int Main(string[] args)
			{
			int returnValue = -1;

			// catch everything
			try
				{
				// check for help request
				if ((args.Length == 0) || (args[0] == "-?") || (args[0] == "--help"))
					{
					Console.WriteLine(BANNER_MESSAGE);
					Console.WriteLine(HELP_MESSAGE);
					}

				else
					{ // execute
					int starttime, lastSaveTime;
					starttime = lastSaveTime = Environment.TickCount;

					bool showPNG = false; // default PPM
					if ((args.Length == 2) && (args[1].ToUpper() == "G"))
						showPNG = true;
					Console.WriteLine(BANNER_MESSAGE);

					// get file names
					string modelFilePathname = args[0];
					string imageFilePathname = Path.GetFileNameWithoutExtension(modelFilePathname);
					if (showPNG == true)
						imageFilePathname += ".png";
					else
						imageFilePathname += ".ppm";

					// open model file
					StreamReader modelFile = File.OpenText(modelFilePathname);

					// check model file format identifier at start of first line
					string formatId = modelFile.ReadLine();
					if (MODEL_FORMAT_ID != formatId)
						throw new Exception("Invalid model file");

					// read frame iterations
					int iterations = (int)modelFile.ReadFloat();

					// create top-level rendering objects with model file
					Image image = new Image(modelFile);
					Camera camera = new Camera(modelFile);
					Scene scene = new Scene(modelFile, camera.ViewPosition);

					modelFile.Close();
					Console.WriteLine("Rendering scene file " + modelFilePathname);
					Console.WriteLine("Output file will be " + imageFilePathname);						

					var rand = new DotNetSampler(); // todo - option to set seed?

					// do progressive refinement render loop
					for (int frameNo = 1; frameNo <= iterations; ++frameNo)
						{
						// render a frame
						camera.GetFrame(scene, rand, image);

						// display latest frame number
						Console.CursorLeft = 0;
						Console.Write("Iteration: {0} of {1}. Elapsed seconds {2}", frameNo, iterations, (Environment.TickCount-lastSaveTime)/1000);

						// save image every three minutes, and at end
						if ((frameNo == iterations) || (Environment.TickCount - lastSaveTime > SAVE_PERIOD * 1000))
							{
							lastSaveTime = Environment.TickCount;
							image.SaveImage(imageFilePathname, frameNo, showPNG);
							if (frameNo == iterations)
								Console.WriteLine("\nImage file {0} saved", imageFilePathname);
							}

						}

					Console.WriteLine("\nfinished in {0} secs",(Environment.TickCount - starttime)/1000.0);
					}

				returnValue = 1;
				}
			// print exception message
			catch (Exception e)
				{
				Console.WriteLine("\n*** execution failed:  " + e.Message);
				}
			return returnValue;
			}

		/// user messages --------------------------------------------------------------
		static string BANNER_MESSAGE =
		"\n----------------------------------------------------------------------\n" +
		"  MiniLight v0.9 C# 3.5\n\n" +
		"  Copyright (c) 2009, Chris Lomont\n" +
		"  http://www.lomont.org\n\n" +
		"  2009-03-26\n" +
		"----------------------------------------------------------------------";

		static string HELP_MESSAGE =
		"  Based on MiniLight 1.5.2 C++\n" +
		"  Copyright (c) 2006-2008, Harrison Ainsworth / HXA7241.\n" +
		"  http://www.hxa7241.org/minilight/\n" +
		"----------------------------------------------------------------------\n\n" +
		"MiniLight is a minimal global illumination renderer.\n\n" +
		"usage:\n" +
		"  minilight modelFilePathName [g]\n\n" +
		"     where optinal g denotes save as PNG, else saves as PPM\n"+
		"The model text file format is:\n" +
		"  #MiniLight\n" +
		"  iterations\n" +
		"  imagewidth imageheight\n" +
		"  viewposition viewdirection viewangle\n" +
		"  skyemission groundreflection\n" +
		"  vertex0 vertex1 vertex2 reflectivity emitivity\n" +
		"  vertex0 vertex1 vertex2 reflectivity emitivity\n" +
		"  ...\n" +
		"\n" +
		"-- where iterations and image values are ints, viewangle is a float,\n" +
		"and all other values are three parenthised floats. The file must end\n" +
		"with a newline. Eg.:\n" +
		"  #MiniLight\n" +
		"  100\n" +
		"  200 150\n" +
		"  (0 0.75 -2) (0 0 1) 45\n" +
		"  (3626 5572 5802) (0.1 0.09 0.07)\n" +
		"  (0 0 0) (0 1 0) (1 1 0)  (0.7 0.7 0.7) (0 0 0)\n";
		}
	}
