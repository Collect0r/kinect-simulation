﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDummy
{
    public class DepthFrameSource
    {
        public FrameDescription FrameDescription { get; }
        private DepthFrameReader depthFrameReader;

        internal DepthFrameSource(Microsoft.Kinect.KinectSensor realKinectSensor)
        {
            FrameDescription = new FrameDescription();
            depthFrameReader = new DepthFrameReader(realKinectSensor);
        }

        internal DepthFrameSource()
        {
            FrameDescription = new FrameDescription();
            depthFrameReader = new DepthFrameReader();
        }

        public DepthFrameReader OpenReader()
        {
            return depthFrameReader.Open();
        }
    }
}
