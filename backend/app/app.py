import os
from flask import Flask, g, json, request, jsonify
from flask.helpers import make_response
from flask_cors import CORS
from flask_sqlalchemy import SQLAlchemy
from sqlalchemy.ext.serializer import loads, dumps

from models import user
from models.user import User

app = Flask(__name__)
app.config.from_mapping(
    SECRET_KEY='secret-key',
    DATABASE=os.path.join(app.instance_path, 'app.sqlite'),
    SQLALCHEMY_TRACK_MODIFICATIONS=False,
    SQLALCHEMY_DATABASE_URI=
    'sqlite:////home/chikai/vue_learn/vue_shop/backend/app/models/user.db',
)

# ensure the instance folder exists
try:
    os.makedirs(app.instance_path)
except OSError:
    pass
# 跨域
CORS(app, resources=r'/*',expose_headers='total')

db = SQLAlchemy()
db.init_app(app)
user.init_app(app)

# 路由
@app.route('/register', methods=['GET', 'POST'])
def register():
    json = request.get_json()
    user = User(email=json['email'],
                user=json['user'],
                password=json['password'],
                tel=json['tel'],
                admin=json['admin'])
    db.session.add(user)
    try:
        db.session.commit()
    except Exception as s:
        print(s)
        return make_response(s,333)        
    return make_response('ok')



@app.route('/login', methods=['GET','POST'])  # 在出现一系列问题，如Notype 报错等，在向数据库输入数据后消失
def login():
    json = request.get_json()
    u = User.query.filter_by(user=json['user']).first()
    if u is None:  # verify_password 要避免 NOne
        return "None"  #控制返回状态
    else:
        if u.verify_password(json['password']):
            g.currnet_user = u
            token = u.generate_auth_token(expiration=3600)
            return token


@app.route('/user', methods=['GET', 'POST'])
def getuserdate():
    s = request.args.get("s")
    p = request.args.get("p")
    q = request.args.get("q")
    qy = User.query.filter(User.user.startswith(q))
    res = qy.paginate(int(p), int(s))
    resp = make_response(jsonify(res.items))
    resp.headers["total"] = qy.count()
    return resp

@app.route('/userchange',methods=['GET','POST']) # 终止后重启服务发现数据没有修改，可能是手动终止服务导致的
def userchange():
    json = request.get_json()
    u = User.query.filter_by(id=json['id']).first()
    if u is None:
        return make_response('error',333)
    
    if u.admin == 'true':
        u.admin = 'false'
        try:
           db.session.commit()
        except:
           return make_response('error',333)        
        return make_response('ok')

    if u.admin == 'false':
        u.admin = 'true'
        try:
           db.session.commit()  # 重启后恢复，并没有修改
        except:
           return make_response('error',333)        
        return make_response('ok')

@app.route('/edit', methods=['GET', 'POST'])
def edit():
    json = request.get_json()
    u = json['user']
    t = json['tel']
    e = json['email']
    res = User.query.filter_by(user=u).first()
    res.tel = t
    res.email = e 
    try:
        db.session.commit()
    except:
        return make_response('error',333)        
    return make_response('ok')

@app.route('/deleteuser', methods=['GET', 'POST'])
def deleteuser():
    json = request.get_json()
    i = json['id']
    res = User.query.filter_by(id=i).first()
    try:
        # https://stackoverflow.com/questions/24291933/sqlalchemy-object-already-attached-to-session
        # 一个很好的解决方法是提取当前绑定的会话并使用它
        # 解决 bug : Object '<User at 0x7f12375ccbb0>' is already attached to session '1' (this is '2')
        current_db_sessions = db.session.object_session(res) 
        current_db_sessions.delete(res)
        current_db_sessions.commit()
    except Exception as e:
        print(e)
        return make_response('error',333)        
    return make_response('ok')

