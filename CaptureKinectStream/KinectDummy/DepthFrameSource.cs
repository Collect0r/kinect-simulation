﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDummy
{
    public class DepthFrameSource
    {
        public FrameDescription FrameDescription { get; set; }
        private DepthFrameReader depthFrameReader;

        public DepthFrameSource()
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