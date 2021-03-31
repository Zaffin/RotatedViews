#include "pch.h"

#include "GraphicsService.h"

GraphicsServiceWrapper::GraphicsService::GraphicsService()
{

}

int GraphicsServiceWrapper::GraphicsService::DrawView(Matrix3D viewMatrix, Point3D origin)
{
	auto id = GrTempAddAxes(CreateNativeMatrix(viewMatrix), CreateNativePoint(origin));

	GrTempDraw(id);

	return id;
}

void GraphicsServiceWrapper::GraphicsService::EraseView(int id)
{
	GrTempRemove(id);
}

GraphicsServiceWrapper::GraphicsService::~GraphicsService()
{

}

matrix_33 GraphicsServiceWrapper::GraphicsService::CreateNativeMatrix(Matrix3D matrix)
{
	auto nativeMatrix = matrix_33{};
	
	nativeMatrix[0] = CreateNativePoint(matrix.Row1);
	nativeMatrix[1] = CreateNativePoint(matrix.Row2);
	nativeMatrix[2] = CreateNativePoint(matrix.Row3);

	return nativeMatrix;
}

p_3d GraphicsServiceWrapper::GraphicsService::CreateNativePoint(Point3D point)
{
	auto nativePoint = p_3d{};

	nativePoint.Set(point.x, point.y, point.z);

	return nativePoint;
}
