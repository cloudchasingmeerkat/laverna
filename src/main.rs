use bevy::{
    prelude::*,
    render::settings::WgpuSettings,
};

#[derive(Resource)]
struct GameState {
    counter: usize
}

/**
fn spawn_tasks(mut commands: Commands) {
    let thread_pool = AsyncComputeTaskPool::get();
    for _x in 0..3 {
        let task = thread_pool.spawn(async move{
            let y: String = String::from("AI");
            y
        });
        commands.spawn(ComputePrint(task));
    }
}
*/

/*
struct PlayerSpawnEvent(Entity);

fn spawn_player(mut commands: Commands) {
    commands.spawn(Player(0));
}

fn player_level_up(
    mut ev_levelup: EventWriter<PlayerSpawnEvent>,
    query: Query<(Entity,&Player)>
) {
    for (entity, _xp) in query.iter() {
        ev_levelup.send(PlayerSpawnEvent(entity));
    }
}
*/

fn main() {
    App::new()
        .insert_resource(WgpuSettings{backends: None,..default()})
        .insert_resource(GameState {counter: 10})
        .add_plugins(DefaultPlugins)
        .add_stage("AI",SystemStage::parallel())
        .add_stage("SRS",SystemStage::parallel());
        .run();
}
