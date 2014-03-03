def mainf(str):
    eval(str)
def getvalue(name):
    data=True
    try:
        data=eval(name)
    except SyntaxError:
        data=False
    finally:
        return data
def Equal(numb1,numb2):
    if numb1==numb2:
        return 1
    if numb1!=numb2:
        return 0
def IF(condition, true_part, false_part):
    return (condition and [true_part] or [false_part])[0]
def ABS(num):
    data=num
    if(num<0):
        data=-num
    return data
def PI():
    return 3.1415926