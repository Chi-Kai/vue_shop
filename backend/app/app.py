import os

from flask import Flask, g, request
from flask_cors import CORS
from flask_sqlalchemy import SQLAlchemy

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
CORS(app, resources=r'/*')

db = SQLAlchemy()
db.init_app(app)
user.init_app(app)


def register():
    json = request.get_json()
    email = json['user'] + '@email.com'
    user = User(email=email, user=json['user'], password=json['password'])
    db.session.add(user)
    db.session.commit()
    print('OK')
    return 'ok'


@app.route('/login', methods=['GET',
                              'POST'])  # 在出现一系列问题，如Notype 报错等，在向数据库输入数据后消失
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
    return "None"
