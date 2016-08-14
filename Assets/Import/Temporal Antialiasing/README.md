# Temporal Reprojection Anti-Aliasing

Source code release of the anti-aliasing solution used in Playdead's *INSIDE*.

#### LICENSE
MIT (see [LICENSE.txt](LICENSE.txt))

#### REQUIRES
Unity 5.0+

#### INSTRUCTIONS
- copy Assets/* to your project
- disable MSAA under 'Project Settings/Quality'
- add the TemporalReprojection component to your cameras
- add the VelocityBufferTag component to individual moving meshes (if you want correct motion vectors)
- for skinned meshes: tick the skinned mesh option in the tag
- tagging skinned meshes is expensive

#### AUTHOR
Lasse Jon Fuglsang Pedersen <<lasse@playdead.com>>

#### THANKS TO
* Mikkel Gjøl (suggestions and feedback, noise distributions, motion blur tweaks)
* Tiago Sousa (neighbourhood clamping in SMAA 1tx; http://www.crytek.com/download/Sousa_Graphics_Gems_CryENGINE3.pdf)
* Brian Karis (YCoCg clipping, neighbourhood rounding; http://advances.realtimerendering.com/s2014/epic/TemporalAA.pptx)
* Timothy Lottes (weighing by unbiased luminance diff; http://www.youtube.com/watch?v=WzpLWzGvFK4&t=18m)
* Morgan McGuire (motion blur reconstruction filter; http://graphics.cs.williams.edu/papers/MotionBlurI3D12/McGuire12Blur.pdf)
