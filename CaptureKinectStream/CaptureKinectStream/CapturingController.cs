﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Kinect;
using KinectDummy;
using System.Timers;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace CaptureKinectStream
{
    public class CapturingController
    {
        private bool currentlyCapturing = false;
        private KinectSensor kinect;
        private DepthFrameReader depthFrameReader;
        private DepthFrame depthFrame;
        private ushort[] depthFrameAsArray;
        private int width;
        private int height;
        private Stopwatch sw;
        private bool firstFrame = true;
        private int secondsToCapture;

        public CapturingController(KinectSensor kinect)
        {
            if (kinect == null)
                throw new InvalidOperationException("Can't start capturing kinect frames because KinectSensor was null.");

            this.kinect = kinect;
        }
        
        public void startCapturing(String filePath, int secondsToCapture = 0)
        {
            this.secondsToCapture = secondsToCapture;

            width = kinect.DepthFrameSource.FrameDescription.Width;
            height = kinect.DepthFrameSource.FrameDescription.Height;
            depthFrameAsArray = new ushort[width * height];
            
            sw = new Stopwatch();

            DataStreamHandler.startWritingQueueToFile(filePath);

            depthFrameReader = kinect.DepthFrameSource.OpenReader();
            depthFrameReader.FrameArrived += depthDataReadyEventHandler;
        }
        
        public void stopCapturing()
        {
            DataStreamHandler.requestWriteStop();
            depthFrameReader.FrameArrived -= depthDataReadyEventHandler;
            Console.WriteLine("STOPPED");

            // gets also called in real code??? think nope
            //depthFrameReader.Dispose();
        }

        private void depthDataReadyEventHandler(object sender, DepthFrameArrivedEventArgs e)
        {
            if (firstFrame)
            {
                firstFrame = false;
                sw.Start();
            }

            // if secondsToCapture == 0 then the capturing stops only if the user clicks the stop-button
            if (secondsToCapture > 0 && sw.ElapsedMilliseconds > 1000 * secondsToCapture)
            {
                stopCapturing();
            }
            else
            {
                depthFrameReader.AcquireLatestFrame().CopyFrameDataToArray(ref depthFrameAsArray);
                
                DataStreamHandler.addFrameToQueue(depthFrameAsArray);
            }


        }
    }
}