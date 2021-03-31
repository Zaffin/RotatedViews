#pragma once

using namespace Mastercam::Math;

namespace GraphicsServiceWrapper
{
	public ref class GraphicsService
	{
	public:
		GraphicsService();

		int DrawView(Matrix3D viewMatrix, Point3D origin);

		void EraseView(int id);

		~GraphicsService();

	private:
		matrix_33 CreateNativeMatrix(Matrix3D matrix);
		p_3d CreateNativePoint(Point3D point);
	};

}