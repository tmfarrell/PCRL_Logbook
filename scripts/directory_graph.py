## 
##  directory_graph.py  
##  
##  - produces crude graphical visualization of 
##  file to file reference/calling structure
##  of code files in all subdirs of dir_
##  
##  - found useful for getting quickly aquianted
##  with complex/massive packages/codebases 
## 
##  input:  dir_path = '/path/to/dir' 
##  output: dir_path + '/' + dir_'.pdf'
## 
##  Tim Farrell, tfarrell01@gmail.com 
##  20160214
## 
import os
import sys 
import graphviz as gv


# simple file class 
class File(): 
    def __init__(self, fname, path): 
        self.name = fname[:fname.rfind('.')]
        self.ext = fname[fname.rfind('.') + 1:]
        self.path = path

    def __eq__(self, other): 
        return (self.full_path() == self.full_path())
    
    def __hash__(self): 
        return hash(self.full_path())
    
    def __str__(self): 
        return self.full_path()
    
    def full_path(self): 
        return (self.path + '/' + self.name + '.' + self.ext)  
    
    def text(self): 
        f = open(self.full_path(), 'r')
        text = f.read(); f.close(); 
        return text 
    
    
# is file_ code? 
def iscode(file_): 
    code_exts = ['py','sh','c','h', 'cpp', 'html', 'md', 'cs', 'xml', 'json']
    return (file_[file_.rfind('.') + 1:] in code_exts) 


## MAIN
# dir of interest 
dirpath = sys.argv[1]
dirname = dirpath[dirpath.rfind('/') + 1:]

# get all code files (as defined by iscode()) in all subdirs
codefiles = [File(fname, path) for (path, dirs, fnames) in os.walk(dirpath)\
                                for fname in fnames if iscode(fname)] 

# get all reference pairs 
references = [(from_, to_) for from_ in codefiles for to_ in codefiles \
              if (to_.name in from_.text() and to_ != from_)] # self-refer not allowed 

# build graph 
f2f_graph = gv.Digraph(dirname)
nodes = map(lambda f: f2f_graph.node(f.name), codefiles)
edges = map(lambda (from_, to_): f2f_graph.edge(from_.name, to_.name), references)

# save it 
f2f_graph.render(filename=dirname + '.pdf')