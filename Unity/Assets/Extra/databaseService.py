#!/usr/bin/env python
# -*- coding: utf-8 -*-
import sqlite3
from bottle import route, run, template, request

connection = None
cursor = None

@route('/hello/<name>')
def index(name):
    return template('<b>Hello {{name}}</b>!', name=name)

@route('/test/electricity', method='POST')
def index():
    postdata = request.body.read()
    print postdata #this goes to log file only, not to client
    timestamp = request.forms.get("timestamp")
    phaseId = request.forms.get("phaseId")
    activePower = request.forms.get("activePower")
    reactivePower = request.forms.get("reactivePower")
    cursor.execute("insert into Electricity (timestamp, phaseId, activePower, reactivePower) values (?,?,?,?)", (timestamp, phaseId, activePower, reactivePower))
    connection.commit()
    return "200"

@route('/test/rfid', method='POST')
def index():
    postdata = request.body.read()
    print postdata #this goes to log file only, not to client
    timestamp = request.forms.get("timestamp")
    phaseId = request.forms.get("phaseId")
    return "Hi {timestamp} {phaseId}".format(timestamp=timestamp, phaseId=phaseId)


connection = sqlite3.connect("smarthome.db")
cursor = connection.cursor()
cursor.execute("create table if not exists Electricity (timestamp character(255), phaseId character(2), activePower integer, reactivePower integer)")
connection.commit()
cursor.execute("create table if not exists RFID (timestamp character(255), antenaId character(255), signalStrenght integer, tagId character(255))")
connection.commit()

run(host='localhost', port=8080, debug=True)