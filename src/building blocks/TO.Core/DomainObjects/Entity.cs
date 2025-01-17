﻿using TO.Core.Messages;

namespace TO.Core.DomainObjects;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();


    private List<Event> _notificacoes;

    public IReadOnlyCollection<Event> Notificacoes => _notificacoes?.AsReadOnly();

    public void AdicionarEvento(Event evento)
    {
        _notificacoes ??= [];
        _notificacoes.Add(evento);
    }

    public void RemoverEvento(Event eventItem)
        => _notificacoes?.Remove(eventItem);

    public void LimparEventos()
        => _notificacoes?.Clear();

    #region Comparações
    public override bool Equals(object? obj)
    {
        var compareTo = obj as Entity;
        if (ReferenceEquals(this, compareTo)) return true;
        if (ReferenceEquals(null, compareTo)) return false;

        return Id.Equals(compareTo.Id);
    }

    public override int GetHashCode() => (GetType().GetHashCode() * 907) + Id.GetHashCode();
    public override string ToString() => $"{GetType().Name} [Id={Id}]";

    public static bool operator ==(Entity a, Entity b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
        if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

        return a.Equals(b);
    }
    public static bool operator !=(Entity a, Entity b) => !(a == b);
    #endregion
}
