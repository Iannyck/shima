#!/usr/bin/env python
# -*- coding: utf-8 -*-

from bottle import route, run, template, request

@route('/hello/<name>')
def index(name):
    return template('<b>Hello {{name}}</b>!', name=name)

@route('/test/electricity', method='POST')
def index():
    postdata = request.body.read()
    print(postdata) #this goes to log file only, not to client
    timestamp = request.forms.get("timestamp")
    phaseId = request.forms.get("phaseId")
    return "Hi {timestamp} {phaseId}".format(timestamp=timestamp, phaseId=phaseId)

@route('/test/rfid', method='POST')
def index():
    postdata = request.body.read()
    print(postdata) #this goes to log file only, not to client
    timestamp = request.forms.get("timestamp")
    phaseId = request.forms.get("phaseId")
    return "Hi {timestamp} {phaseId}".format(timestamp=timestamp, phaseId=phaseId)

run(host='localhost', port=8080, debug=True)
