TEMPLATE = app
CONFIG += console c++11 c++14
CONFIG -= app_bundle
CONFIG -= qt

LIBS += \
	-L/usr/lib \
	-lprotobuf \
	-lgrpc++ \

SOURCES += \
	main.cpp \
	*.pb.*

DISTFILES += \
	Makefile \
	sample.proto
