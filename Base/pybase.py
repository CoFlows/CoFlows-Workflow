#py-pybase
class pybase:
    getName = "something"

    getAge2 = 22.5

    def getAge3(self):
        return 22.5

    def _getAge(self):
        return 20.5

    getAge = property(fget = _getAge)

    def Add(self, x, y):
        return x + y

    def getInterests(self):
        return ["F#", "C#", "Vb", "Java", "Scala", "Python", "Javascript"]
