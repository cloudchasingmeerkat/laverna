use bevy::prelude::*;
use bevy_rapier3d::prelude::*;

#[derive(Component)]
struct Marker(bool);

#[derive(States,Hash,Clone,PartialEq,Eq,Debug,Default)]
enum AppState {
    #[default]
    DemoScene,
    MissionSuccess,
    MissionFailure,
    CleanUp
}

fn setup_demo_scene(mut commands: Commands, asset_server: Res<AssetServer>) {
    // TRIGGERS THE ERROR
    commands.spawn(Collider::cuboid(0.5,0.5,0.5));
    commands.spawn(SceneBundle {
        scene: asset_server.load("models/VisibleCube.glb#Scene0"),
        ..default()
    }).with_children(|children| {
        children.spawn(Collider::cuboid(0.5,0.5,0.5));
    });
    commands.spawn(PointLightBundle {
        transform: Transform::from_xyz(0.0,0.0,4.0),
        point_light: PointLight {
            intensity: 1000.0,
            color: Color::GREEN,
            shadows_enabled: false,
            ..default()
        },
        ..default()
    });
    commands.spawn(Camera3dBundle {
        transform: Transform::from_xyz(0.0,0.0,10.0).looking_at(Vec3::new(0.0,0.0,0.0),Vec3::Y),
        ..default()
    });
}

fn main() {
    App::new()
        .add_plugins(DefaultPlugins)
        .add_startup_system(setup_demo_scene)
        .run();
}
