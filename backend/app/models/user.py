from dataclasses import dataclass
from flask_sqlalchemy import SQLAlchemy
import click
from flask.cli import with_appcontext
from werkzeug.security import generate_password_hash, check_password_hash
from itsdangerous import TimedJSONWebSignatureSerializer as Serializer
from flask import current_app

db = SQLAlchemy()

@dataclass
class User(db.Model):
    id : int
    user : str
    email: str
    tel : str
    admin : bool
    
    id = db.Column(db.Integer, primary_key=True)
    user = db.Column(db.String(20), unique=True, index=True)
    email = db.Column(db.String(64), unique=True, index=True)
    tel   = db.Column(db.String(64), unique=True, index=True)
    password_hash = db.Column(db.String(128))
    admin = db.Column(db.Boolean)


    @property
    def password(self):
        raise AttributeError('password is not a readable attribute')

    @password.setter
    def password(self, password):
        self.password_hash = generate_password_hash(password)

    def verify_password(self, password):
        return check_password_hash(self.password_hash, password)

    def generate_auth_token(self, expiration):  # 产生token
        s = Serializer(current_app.config['SECRET_KEY'], expires_in=expiration)
        return s.dumps({'id': self.id}).decode('utf-8')

    @staticmethod
    def verify_auth_token(token):
        s = Serializer(current_app.config['SECRET_KEY'])
        try:
            data = s.loads(token)
        except:
            return None
        return User.query.get(data['id'])


@click.command('init-db')
@with_appcontext
def init_db_command():
    """Clear the existing data and create new tables."""
    db.create_all()
    click.echo('Initialized the database.')


def init_app(app):
    app.cli.add_command(init_db_command)